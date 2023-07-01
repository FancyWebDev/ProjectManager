<template>
  <form v-on:submit.prevent="create">
    <div class="mb-3">
      <label for="name" class="form-label">Name</label>
      <input type="text" v-model="collaborator.firstName" class="form-control" id="name" aria-describedby="name">
    </div>
    <div class="mb-3">
      <label for="last-name" class="form-label">Last name</label>
      <input type="text" v-model="collaborator.lastName" class="form-control" id="last-name" aria-describedby="last name">
    </div>
    <div class="mb-3">
      <label for="patronymic" class="form-label">patronymic</label>
      <input type="text" v-model="collaborator.patronymic" class="form-control" id="patronymic" aria-describedby="patronymic">
    </div>
    <div class="mb-3">
      <label for="email" class="form-label">Email</label>
      <input type="email" v-model="collaborator.email" class="form-control" id="email" aria-describedby="email">
    </div>
    <button type="submit" class="btn btn-primary">Create</button>
  </form>
</template>

<script>
import { create } from '@/services/collaboratorRequestService.js'

export default {
  data() {
    return {
      collaborator: {}
    }
  },
  methods: {
    create() {
      create(this.collaborator)
      this.$router.push('/collaborator')
    }
  }
}
</script>