<template>
  <nav class="navbar navbar-expand-lg bg-body-tertiary">
    <div class="container-fluid">
      <a class="navbar-brand" href="#">Project Manager</a>
      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent"
              aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav me-auto mb-2 mb-lg-0">
          <li class="nav-item">
            <router-link to="/" class="nav-link">Home</router-link>
          </li>
          <li class="nav-item">
            <router-link :to="{name: 'ProjectIndex'}" class="nav-link">Projects</router-link>
          </li>
          <li class="nav-item">
            <router-link :to="{name: 'CollaboratorIndex'}" class="nav-link">Collaborators</router-link>
          </li>
          <li class="nav-item">
            <router-link :to="{name: 'TaskIndex'}" class="nav-link">Tasks</router-link>
          </li>
          <li class="nav-item">
            <router-link :to="{name: 'ProjectWizard'}" class="nav-link">Wizard</router-link>
          </li>
          <li v-if="isLoggedIn === false" class="nav-item">
            <router-link :to="{name: 'Login'}" class="nav-link">Log in</router-link>
          </li>
          <li v-if="isLoggedIn === false" class="nav-item">
            <router-link :to="{name: 'Register'}" class="nav-link">Register</router-link>
          </li>
          <li v-else class="nav-item">
            <router-link :to="{name: 'Login'}" @click="loggedOut" class="nav-link">Log out</router-link>
          </li>
          <li v-else class="nav-item">
            <router-link :to="{name: 'Login'}" @click="loggedOut" class="nav-link">Log out</router-link>
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>

<script>
import EventBus from '@/helpers/EventBus'
import { logout } from '@/services/accountRequestService'

export default {
  data() {
    return {
      isLoggedIn: false
    }
  },
  mounted() {
    EventBus.on('loggedIn', this.loggedIn)
  },
  methods: {
    loggedIn() {
      this.isLoggedIn = true
    },
    loggedOut() {
      logout()
          .then(_ => this.isLoggedIn = false)
    }
  }
}
</script>