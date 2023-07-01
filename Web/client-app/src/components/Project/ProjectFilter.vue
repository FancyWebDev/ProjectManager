<template>
  <nav class="navbar navbar-expand-md bg-dark sticky-top border-bottom" data-bs-theme="dark">
    <div class="container">
      <div class="offcanvas" tabindex="-1" id="#offcanvas" aria-labelledby="#offcanvasLabel">
        <div class="offcanvas-body">
          <ul class="navbar-nav d-flex flex-grow-1 p-2">
            <form class="d-flex" role="search">
              <input v-model="term"  @keyup="search" class="form-control me-2" type="search" placeholder="Search by name, company name" aria-label="Search">
            </form>
            <button @click="toggleFilterSidebar" class="btn btn-primary" type="button" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight" aria-controls="offcanvasRight">Filter</button>
          </ul>
        </div>
      </div>
    </div>
  </nav>
  <FilterSideBar />
</template>

<script>
import EventBus from '@/helpers/EventBus';
import { getAll } from '@/services/projectRequestService'
import FilterSideBar from '@/components/Project/FilterSidebar.vue'
import {
  applyProjectFilter,
  projectsFiltered,
  toggleProjectFilterSidebar
} from '@/helpers/variables'

export default {
  data() {
    return {
      term: '',
      projects: null,
      filteredProjects: {}
    }
  },
  created() {
    getAll()
      .then(data => this.projects = data)
  },
  mounted() {
    EventBus.on(applyProjectFilter, this.filter)
    this.$forceUpdate();
  },
  components: {
    FilterSideBar
  },
  methods: {
    search() {
      let target
      this.filteredProjects = this.projects.filter(({ name, customerCompanyName, performerCompanyName }) => {
        target = name.concat(customerCompanyName, performerCompanyName)
        return target.toLowerCase().includes(this.term.toLowerCase())
      })

      EventBus.trigger(projectsFiltered, this.filteredProjects)
    },
    filter(projects) {
      this.filteredProjects = projects 
      EventBus.trigger(projectsFiltered, this.filteredProjects)
    },
    toggleFilterSidebar() {
      EventBus.trigger(toggleProjectFilterSidebar, this.projects)
    }
  }
}
</script>