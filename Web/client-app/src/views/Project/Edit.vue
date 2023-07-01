<template>
  <form v-on:submit.prevent="getData">
    <Form />  
    <button type="submit" class="btn btn-primary">Update</button>
  </form>
</template>

<script>
import EventBus from '@/helpers/EventBus'
import Form from '@/components/Project/Form.vue';
import { update } from '@/services/projectRequestService'
import {
  getProjectFormData, provideProjectFormData, setProjectFromData
} from '@/helpers/variables'

export default {
  mounted() {
    EventBus.on(provideProjectFormData, this.update)
    EventBus.trigger(setProjectFromData, this.$route.params.id)
  },
  components: {
    Form
  },
  methods: {
    getData() {
      EventBus.trigger(getProjectFormData)
    },
    update(project) {
      update(project)
      this.$router.push('/project')
    }
  }
}
</script>