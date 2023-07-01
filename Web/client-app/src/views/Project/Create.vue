<template>
  <form v-on:submit.prevent="getData">
    <Form/>
    <button type="submit" class="btn btn-primary">Create</button>
  </form>
</template>

<script>
import EventBus from '@/helpers/EventBus'
import { create } from '@/services/projectRequestService'
import Form from '@/components/Project/Form.vue'
import {
  getProjectFormData, provideProjectFormData
} from '@/helpers/variables'

export default {
  mounted() {
    EventBus.on(provideProjectFormData, this.create)
  },
  components: {
    Form
  },
  methods: {
    getData() {
      EventBus.trigger(getProjectFormData)
    },
    create(project) {
      create(project)
      console.log(JSON.stringify(project))
      this.$router.push('/project')
    }
  }
}
</script>

<style scoped>
form {
  width: 70%
}
</style>