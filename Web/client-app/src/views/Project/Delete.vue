<template>
  <form v-on:submit.prevent="this.delete">
    <div class="mb-3">
      <label for="name" class="form-label">Name</label>
      <input disabled type="text" v-model="project.name" class="form-control" id="name" aria-describedby="name">
    </div>
    <div class="mb-3">
      <label for="customer-company-name" class="form-label">Customer Company Name</label>
      <input disabled type="text" v-model="project.customerCompanyName" class="form-control" id="customer-company-name"
             aria-describedby="customer company name">
    </div>
    <div class="mb-3">
      <label for="performer-company-name" class="form-label">Performer Company Name</label>
      <input disabled type="text" v-model="project.performerCompanyName" class="form-control" id="performer-company-name"
             aria-describedby="performer company name">
    </div>
    <div class="mb-3">
      <label for="start-date" class="form-label">Start Date</label>
      <input disabled type="text" v-model="project.projectStartDate" class="form-control" id="start-date"
             aria-describedby="end date">
    </div>
    <div class="mb-3">
      <label for="end-date" class="form-label">End Date</label>
      <input disabled type="text" v-model="project.projectEndDate" class="form-control" id="end-date"
             aria-describedby="end date">
    </div>
    <div class="mb-3">
      <label for="priority" class="form-label">Priority</label>
      <select v-model="rawData.priority" id="priority" class="form-control">
        <option v-for="option of priorityOptions" :selected="rawData.priority">{{ option.priorityName }}</option>
      </select>
    </div>
    <div class="mb-3">
      <label class="form-label">Collaborators</label>
      <div class="row row-cols-3 m-3 gap-3">
        <div v-for="collaborator in project.collaborators" class="card" style="width: 18rem;">
          <div class="card-body">
            <h6 class="card-title">{{ collaborator.firstName }} {{ collaborator.lastName }} {{ collaborator.patronymic }}
            </h6>
            <p class="card-text">{{ collaborator.email }}</p>
          </div>
        </div>
      </div>
    </div>
    <div class="mb-3">
      <label class="form-label">Director</label>
      <div v-if="rawData.director != null" class="m-3 d-flex justify-content-center">
        <div class="card" style="width: 18rem;">
          <div class="card-body">
            <h6 class="card-title">{{ rawData.director.firstName }} {{ rawData.director.lastName }} {{
                rawData.director.patronymic
              }}
            </h6>
            <p class="card-text">{{ rawData.director.email }}</p>
          </div>
        </div>
      </div>
    </div>
    <button type="submit" class="btn btn-danger">Delete</button>
  </form>
</template>

<script>
import { deleteProject, get } from '@/services/projectRequestService'

export default {
  data() {
    return {
      project: {},
      rawData: {
        collaborators: {},
        priority: null,
        director: null
      },
      priorityOptions: {
        'None': {
          priorityName: 'None',
          priority: 0
        },
        'Medium': {
          priorityName: 'Medium',
          priority: 1
        },
        'High': {
          priorityName: 'High',
          priority: 2
        }
      }
    }
  },
  created() {
    get(this.$route.params.id).then(data => {
      this.project = data
      this.rawData.priority = Object.values(this.priorityOptions)[this.project.priority].priorityName
      this.rawData.director = this.project.collaborators
                                  .find(collaborator => collaborator.id === this.project.directorId)
    })
  },
  methods: {
    delete() {
      deleteProject(this.project)
      this.$router.push('/project')
    }
  }
}
</script>