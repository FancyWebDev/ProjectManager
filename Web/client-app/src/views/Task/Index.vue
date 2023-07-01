<template>
  <main>
    <section class="py-5 text-center container">
      <div class="row py-lg-5">
        <div class="col-lg-6 col-md-8 mx-auto">
          <h1 class="fw-light">Tasks</h1>
          <p class="lead text-body-secondary">Something short and leading about the collection below—its contents, the
            creator, etc. Make it short and sweet, but not too short so folks don’t simply skip over it entirely.</p>
          <p>
            <router-link :to="{ name: 'TaskCreate' }" class="btn btn-success">Create</router-link>
          </p>
        </div>
      </div>
    </section>

    <TaskFilter />
    <div class="album py-5 bg-body-tertiary">
      <div class="container">

        <table class="table">
          <thead>
            <tr>
              <th scope="col">#</th>
              <th scope="col">Name</th>
              <th scope="col">Priority</th>
              <th scope="col">Status</th>
              <th scope="col">Performer</th>
              <th scope="col">Author</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="task of filteredTasks">
              <th scope="row">{{ task.id }}</th>
              <td>{{ task.name }}</td>
              <td>{{ task.priority }}</td>
              <td>{{ task.status }}</td>
              <td>{{ task.performer.firstName }} {{ task.performer.lastName }}</td>
              <td>{{ task.author.firstName }} {{ task.author.lastName }}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </main>
</template>

<script>
import TaskFilter from '@/components/Task/TaskFilter.vue'
import EventBus from '@/helpers/EventBus'
import { getAll } from '@/services/taskRequestService'

export default {
  data() {
    return {
      filteredTasks: null
    }
  },
  created() {
    getAll()
      .then(data => this.filteredTasks = data)
  },
  mounted() {
    EventBus.on('tasksFiltered')
  },
  components: {
    TaskFilter
  },
  methods: {
    setFilteredTasks(tasks) {
      this.filteredTasks = tasks
    }
  }
}
</script>