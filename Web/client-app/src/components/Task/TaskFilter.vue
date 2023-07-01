<template>
  <nav class="navbar navbar-expand-md bg-dark sticky-top border-bottom" data-bs-theme="dark">
    <div class="container">
      <div class="offcanvas" tabindex="-1" id="#offcanvas" aria-labelledby="#offcanvasLabel">
        <div class="offcanvas-body">
          <ul class="navbar-nav d-flex flex-grow-1 p-2">
            <form class="d-flex" role="search">
              <input v-model="term" @keyup="search" class="form-control me-2" type="search"
                placeholder="Search by name" aria-label="Search">
            </form>
            <button @click="toggleFilterSidebar" class="btn btn-primary" type="button" data-bs-toggle="offcanvas"
              data-bs-target="#offcanvasRight" aria-controls="offcanvasRight">Filter</button>
          </ul>
        </div>
      </div>
    </div>
  </nav>
  <FilterSidebar />
</template>

<script>
import FilterSidebar from '@/components/Task/FilterSidebar.vue'
import EventBus from '@/helpers/EventBus';
import { getAll } from '@/services/taskRequestService'

export default {
  data() {
    return {
      term: '',
      tasks: null,
      filteredTasks: {}
    }
  },
  created() {
    getAll()
      .then(data => this.tasks = data)
  },
  components: {
    FilterSidebar
  },
  methods: {
    search() {
      let target
      this.filteredTasks = this.projects.filter(({ name }) => {
        return name.toLowerCase().includes(this.term.toLowerCase())
      })

      EventBus.trigger('tasksFiltered', this.filteredTasks)
    },
    toggleFilterSidebar() {
      EventBus.trigger('toggleTaskFilterSidebar', this.filteredTasks)
    }
  }
}
</script>