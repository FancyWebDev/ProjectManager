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
        <h3 class="my-3">{{ collaborator.email }}</h3>
        <p></p>
        <h3 class="my-3">Full name</h3>
        <ul>
          <li>First name: {{ collaborator.firstName }}</li>
          <li>Last name: {{ collaborator.lastName }}</li>
          <li>Patronymic: {{ collaborator.patronymic }}</li>
        </ul>
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
import { get } from '@/services/collaboratorRequestService.js'

export default {
  data() {
    return {
      collaborator: {}
    }
  },
  created() {
    get(this.$route.params.id)
      .then(data => this.collaborator = data)
  }
}
</script>