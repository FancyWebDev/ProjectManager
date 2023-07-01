<template>
  <main>
    <section class="py-5 text-center container">
      <div class="row py-lg-5">
        <div class="col-lg-6 col-md-8 mx-auto">
          <h1 class="fw-light">Projects</h1>
          <p class="lead text-body-secondary">Something short and leading about the collection below—its contents, the
            creator, etc. Make it short and sweet, but not too short so folks don’t simply skip over it entirely.</p>
          <p>
            <router-link :to="{ name: 'ProjectCreate' }" class="btn btn-success">Create</router-link>
          </p>
        </div>
      </div>
    </section>

    <ProjectFilter />

    <div class="album py-5 bg-body-tertiary">
      <div class="container">

        <div class="row row-cols-1 row-cols-sm-2 row-cols-md-3 g-3">
          <div v-for="{ id, name, customerCompanyName, performerCompanyName } in filteredProjects" :key="id" class="col">
            <div class="card shadow-sm">
              <svg class="bd-placeholder-img card-img-top" width="100%" height="225" xmlns="http://www.w3.org/2000/svg"
                role="img" aria-label="Placeholder: Thumbnail" preserveAspectRatio="xMidYMid slice" focusable="false">
                <title>Placeholder</title>
                <rect width="100%" height="100%" fill="#55595c" /><text x="50%" y="50%" fill="#eceeef"
                  dy=".3em">Thumbnail</text>
              </svg>
              <div class="card-body">
                <h5 class="card-title">{{ name }}</h5>
                <p class="card-text">Performer company: {{ performerCompanyName }}</p>
                <p class="card-text">Customer company: {{ customerCompanyName }}</p>
                <div class="d-flex justify-content-between align-items-center">
                  <div class="d-grid d-md-block">
                    <router-link :to="{ name: 'ProjectDetail', params: { id: id } }"
                      class="btn btn-sm btn-info  me-md-2">View</router-link>
                    <router-link :to="{ name: 'ProjectEdit', params: { id: id } }"
                      class="btn btn-sm btn-warning me-md-2">Edit</router-link>
                    <router-link :to="{ name: 'ProjectDelete', params: { id: id } }"
                      class="btn btn-sm btn-danger">Delete</router-link>
                  </div>
                  <small class="text-body-secondary">9 mins</small>
                </div>
              </div>
            </div>
          </div>
        </div>

      </div>
    </div>
  </main>
</template>

<script>
import { getAll } from '@/services/projectRequestService'
import ProjectFilter from '@/components/Project/ProjectFilter.vue'
import EventBus from '@/helpers/EventBus'

export default {
  data() {
    return {
      filteredProjects: null
    }
  },
  created() {
    getAll()
      .then(data => {
        this.filteredProjects = data
      })
  },
  mounted() {
    EventBus.on('projectsFiltered', this.setFilteredProjects)
    this.$forceUpdate();
  },
  components: {
    ProjectFilter
  },
  methods: {
    setFilteredProjects(projects) {
      this.filteredProjects = projects
    }
  }
}
</script>