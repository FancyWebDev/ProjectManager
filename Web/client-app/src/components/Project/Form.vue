<template>
  <div class="mb-3">
    <label for="name" class="form-label">Name</label>
    <input type="text" v-model="project.name" class="form-control" id="name" aria-describedby="name">
  </div>
  <div class="mb-3">
    <label for="customer-company-name" class="form-label">Customer Company Name</label>
    <input type="text" v-model="project.customerCompanyName" class="form-control" id="customer-company-name"
           aria-describedby="customer company name">
  </div>
  <div class="mb-3">
    <label for="performer-company-name" class="form-label">Performer Company Name</label>
    <input type="text" v-model="project.performerCompanyName" class="form-control" id="performer-company-name"
           aria-describedby="performer company name">
  </div>
  <div class="mb-3">
    <label for="start-date" class="form-label">Start Date</label>
    <input type="date" v-model="project.projectStartDate" class="form-control" id="start-date"
           aria-describedby="end date">
  </div>
  <div class="mb-3">
    <label for="end-date" class="form-label">End Date</label>
    <input type="date" v-model="project.projectEndDate" class="form-control" id="end-date" aria-describedby="end date">
  </div>
  <div class="mb-3">
    <label for="priority" class="form-label">Priority</label>
    <select v-model="rawData.priority" id="priority" class="form-control">
      <option v-for="option of priorityOptions" :selected="rawData.priority">{{ option.priorityName }}</option>
    </select>
  </div>
  <div class="mb-3">
    <label class="form-label">Collaborators</label>
    <button type="button" @click="toggleAddCollaboratorModal" class="btn btn-primary form-control">Add
      Collaborator
    </button>
    <div class="row row-cols-3 m-3 gap-3">
      <div v-for="collaborator in rawData.collaborators" class="card" style="width: 18rem;">
        <div class="card-body">
          <h6 class="card-title">{{ collaborator.firstName }} {{ collaborator.lastName }} {{ collaborator.patronymic }}
          </h6>
          <p class="card-text">{{ collaborator.email }}</p>
          <button @click.prevent="deleteCollaborator(collaborator)" class="btn btn-danger">Delete</button>
        </div>
      </div>
    </div>
    <AddCollaboratorModal/>
  </div>
  <div class="mb-3">
    <label class="form-label">Director</label>
    <SetCollaboratorModal :collaborator="rawData.director" :collaborators="rawData.collaborators" @change="setDirector"/>
    <div v-if="rawData.director != null" class="m-3 d-flex justify-content-center">
      <div class="card" style="width: 18rem;">
        <div class="card-body">
          <h6 class="card-title">{{ rawData.director.firstName }} {{ rawData.director.lastName }}
            {{ rawData.director.patronymic }}
          </h6>
          <p class="card-text">{{ rawData.director.email }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import EventBus from '@/helpers/EventBus'
import AddCollaboratorModal from '@/components/AddCollaboratorModal.vue'
import SetCollaboratorModal from '@/components/SetCollaboratorModal.vue'
import { get } from '@/services/projectRequestService'
import {
  saveSelectedCollaboratorList,
  setProjectFromData,
  getProjectFormData,
  toggleAddCollaboratorModal,
  provideProjectFormData,
  changeSelectedCollaborators
} from '@/helpers/variables'

export default {
  data() {
    return {
      project: {
        name: null,
        customerCompanyName: null,
        performerCompanyName: null,
        directorId: null,
        collaboratorsId: [],
        priority: 0,
        projectStartDate: null,
        projectEndDate: null
      },
      rawData: {
        collaborators: {},
        priority: 'None',
        director: null,
        projectStartDate: new Date(),
        projectEndDate: new Date()
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
  mounted() {
    EventBus.on(saveSelectedCollaboratorList, this.updateCollaboratorList)
    EventBus.on(getProjectFormData, this.provideFormData)
    EventBus.on(setProjectFromData, this.setFormData)
  },
  components: {
    AddCollaboratorModal,
    SetCollaboratorModal
  },
  methods: {
    setFormData(id) {
      get(id)
          .then(data => {
            this.project = data
            this.rawData.priority = Object.values(this.priorityOptions)[this.project.priority].priorityName
            this.rawData.collaborators = this.project.collaborators
            this.project.projectStartDate = this.formatDate(this.project.projectStartDate)
            this.project.projectEndDate = this.formatDate(this.project.projectEndDate)
            console.log(this.project)

            this.rawData.director = Object
                .values(this.rawData.collaborators)
                .find(collaborator => collaborator.id === this.project.directorId)
          })
    },
    formatDate(str) {
      return str
          .replaceAll('/', '-')
          .split('-')
          .reverse()
          .join('-')
    },
    toggleAddCollaboratorModal() {
      EventBus.trigger(changeSelectedCollaborators, Object.assign({}, this.rawData.collaborators))
      EventBus.trigger(toggleAddCollaboratorModal)
    },
    updateCollaboratorList(collaborators) {
      this.rawData.collaborators = collaborators

      if (this.rawData.director == null) return

      const director = Object.values(collaborators)
                             .find(collaborator => collaborator.id === this.rawData.director.id)

      if (director == null)
        delete this.rawData.director
    },
    deleteCollaborator(collaborator) {
      delete this.rawData.collaborators[collaborator.id]

      if (this.rawData.director == null) return

      if (collaborator.id === this.rawData.director.id)
        delete this.rawData.director
    },
    setDirector(director) {
      console.log(director)
      this.rawData.director = director
    },
    provideFormData() {
      this.project.collaboratorsId = Object
          .values(this.rawData.collaborators)
          .map(collaborator => collaborator.id)
      this.project.priority = this.priorityOptions[this.rawData.priority].priority
      this.project.directorId = this.rawData.director.id

      EventBus.trigger(provideProjectFormData, this.project)
    }
  }
}
</script>