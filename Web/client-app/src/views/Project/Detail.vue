<template>
  <div class="container">
    <h1 class="my-4">Page Heading
      <small>Secondary Text</small>
    </h1>

    <div class="row">
      <div class="col-md-8">
        <img class="img-fluid" src="https://via.placeholder.com/500x300" alt="">
      </div>

      <div class="col-md-4">
        <h3 class="my-3">{{ project.name }}</h3>
        <p></p>
        <h3 class="my-3">Details</h3>
        <ul>
          <li>Performer company name {{ project.performerCompanyName }}</li>
          <li>Customer company name: {{ project.customerCompanyName }}</li>
          <li>Priority: {{ project.priority }}</li>
          <li>Project start date: {{ project.projectStartDate }}</li>
          <li>Project end date: {{ project.projectEndDate }}</li>
        </ul>
      </div>
    </div>

    <div class="mb-3">
      <h3 class="form-label">Director</h3>
      <div class="row row-cols-3 m-3 gap-3">
        <p :class="director == null">No director</p>
        <div v-for="collaborator in project.collaborators" class="card" style="width: 18rem;">
          <div class="card-body">
            <h6 class="card-title">{{ director.firstName }} {{ director.lastName }} {{ director.patronymic }}</h6>
            <p class="card-text">{{ director.email }}</p>
            <router-link :to="{ name: 'CollaboratorDetail', params: { id: collaborator.id } }"
                         class="btn btn-sm btn-info  me-md-2">
              View
            </router-link>
          </div>
        </div>
      </div>
    </div>

    <div class="mb-3">
      <h3 class="form-label">Collaborators</h3>
      <div class="row row-cols-3 m-3 gap-3">
        <p :class="project.collaborators == null">No collaborators</p>
        <div v-for="collaborator in project.collaborators"
             v-if="collaborator != null && collaborator.id !== project.directorId"
             class="card" style="width: 18rem;">
          <div class="card-body">
            <h6 class="card-title">{{ collaborator.firstName }} {{ collaborator.lastName }} {{ collaborator.patronymic }}
            </h6>
            <p class="card-text">{{ collaborator.email }}</p>
            <router-link :to="{ name: 'CollaboratorDetail', params: { id: collaborator.id } }"
                         class="btn btn-sm btn-info  me-md-2">View
            </router-link>
          </div>
        </div>
      </div>
    </div>

    <h3 class="my-4">Related Projects</h3>

    <div class="row">
      <div class="col-md-3 col-sm-6 mb-4">
        <a href="#">
          <img class="img-fluid" src="https://via.placeholder.com/500x300" alt="">
        </a>
      </div>

      <div class="col-md-3 col-sm-6 mb-4">
        <a href="#">
          <img class="img-fluid" src="https://via.placeholder.com/500x300" alt="">
        </a>
      </div>

      <div class="col-md-3 col-sm-6 mb-4">
        <a href="#">
          <img class="img-fluid" src="https://via.placeholder.com/500x300" alt="">
        </a>
      </div>

      <div class="col-md-3 col-sm-6 mb-4">
        <a href="#">
          <img class="img-fluid" src="https://via.placeholder.com/500x300" alt="">
        </a>
      </div>
    </div>
  </div>
</template>

<script>
import { get } from '@/services/projectRequestService'

export default {
  data() {
    return {
      project: {},
      director: {}
    }
  },
  created() {
    get(this.$route.params.id)
        .then(data => {
          this.project = data
          this.setDirector(this.project.directorId)
        })
  },
  methods: {
    setDirector(directorId) {
      this.project.collaborators.forEach(collaborator => {
        if (collaborator.id === directorId)
          this.director = collaborator
      })
    }
  }
}
</script>