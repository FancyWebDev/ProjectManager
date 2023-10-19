<template>
  <div class="container text-center">
    <div class="row justify-content-center">
      <div class="col-md-6">
        <h4 class="mt-3 mb-5">Log in to your account</h4>
        <form v-on:submit.prevent="login">
          <div v-if="error != null" class="text-danger">{{ error }}</div>
          <div class="mb-3"><label class="control-label" for="email">Email</label> <input
              v-model="account.email" type="email" id="email" class="form-control"></div>
          <div class="mb-3"><label class="control-label" for="password">Password</label> <input
              v-model="account.password" type="password" id="password" class="form-control"/></div>
          <div class="mb-3 text-center">
            <button class="btn btn-outline-success" type="submit">Log in</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import { login } from '@/services/accountRequestService'
import EventBus from '@/helpers/EventBus'

export default {
  data() {
    return {
      account: {
        email: null,
        password: null
      },
      error: null
    }
  },
  methods: {
    login() {
      login(this.account)
          .then(response => {
            if (response.ok) {
              EventBus.trigger('loggedIn')
              this.$router.push('/collaborator')
              return
            }

            response
                .json()
                .then(data => {
                  this.error = data.detail
                  console.log('error: ' + data.detail)
                })
          })
    }
  }
}
</script>