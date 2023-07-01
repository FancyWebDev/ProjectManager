<template>
  <div class="container">
    <div class="row">
      <div class="col-md-9 offset-2">
        <h4 class="offset-1">Register new account</h4>
        <div class="row">
          <div class="col-md-8">
            <form v-on:submit.prevent="register">
              <div v-if="error != null" class="text-danger">{{ error }}</div>
              <div class="mb-2">
                <label for="name" class="form-label">Name</label>
                <input type="text" v-model="account.firstName" class="form-control" id="name" aria-describedby="name">
              </div>
              <div class="mb-2">
                <label for="last-name" class="form-label">Last name</label>
                <input type="text" v-model="account.lastName" class="form-control" id="last-name" aria-describedby="last name">
              </div>
              <div class="mb-2">
                <label for="patronymic" class="form-label">Patronymic</label>
                <input type="text" v-model="account.patronymic" class="form-control" id="patronymic" aria-describedby="patronymic">
              </div>
              <div class="mb-2">
                <label class="control-label" for="email">Email</label>
                <input v-model="account.email" type="email" id="email" class="form-control">
              </div>
              <div class="mb-2">
                <label class="control-label" for="password">Password</label>
                <input v-model="account.password" type="password" id="password" class="form-control"/>
              </div>
              <div class="mb-2">
                <label class="control-label" for="confirmPassword">Confirm password</label>
                <input v-model="account.confirmPassword" type="password" id="confirmPassword" class="form-control"/>
              </div>
              <div class="mb-2">
                <button class="btn btn-outline-success" type="submit">Register</button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { register } from '@/services/accountRequestService'

export default {
  data() {
    return {
      error: null,
      account: {
        firstName: null,
        lastName: null,
        patronymic: null,
        email: null,
        password: null,
        confirmPassword: null
      }
    }
  },
  methods: {
    register() {
      if (this.account.password !== this.account.confirmPassword) {
        this.error = 'Password and confirm password mismatch'
        return
      }

      register(this.account)
          .then(response => {
            if (response.ok) {
              this.$router.push('/login')
              return
            }

            response
                .json()
                .then(data => {
                  console.log(data)
                  this.error = data.detail
                })
          })
    }
  }
}
</script>