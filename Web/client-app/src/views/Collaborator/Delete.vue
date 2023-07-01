<template>
  <form class="sign-form" v-on:submit="deleteCollaborator">
    <div class="mb-3">
      <label for="name" class="form-label">Name</label>
      <input type="text" v-model="collaborator.firstName" class="form-control" id="name" aria-describedby="name" disabled>
    </div>
    <div class="mb-3">
      <label for="last-name" class="form-label">Last name</label>
      <input type="text" v-model="collaborator.lastName" class="form-control" id="last-name" aria-describedby="last name" disabled>
    </div>
    <div class="mb-3">
      <label for="patronymic" class="form-label">patronymic</label>
      <input type="text" v-model="collaborator.patronymic" class="form-control" id="patronymic" aria-describedby="patronymic" disabled>
    </div>
    <div class="mb-3">
      <label for="email" class="form-label">Email</label>
      <input type="email" v-model="collaborator.email" class="form-control" id="email" aria-describedby="email" disabled>
    </div>
    <button type="submit" class="btn btn-danger">Delete</button>
  </form>
</template>

<script>
import { get, deleteCollaborator } from '@/services/collaboratorRequestService.js'

export default {
  data() {
    return {
      collaborator: {}
    }
  },
  created() {
    get(this.$route.params.id)
      .then(data => this.collaborator = data)
  },
  methods: {
    deleteCollaborator() {
      deleteCollaborator(this.collaborator)
      this.$router.push(`/collaborator`)
    }
  }
}
</script>