<template>
  <div v-if="isActive" class="offcanvas offcanvas-end show" tabindex="-1" id="offcanvasRight"
    aria-labelledby="offcanvasRightLabel" aria-modal="true" role="dialog">
    <div class="offcanvas-header">
      <h5 class="offcanvas-title" id="offcanvasRightLabel">Offcanvas right</h5>
      <button @click="toggle" type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
    </div>
    <div class="offcanvas-body">
      <div class="container-fluid bd-example-row">
        <div class="row row-cols-1 align-items-center">
          <div class="card">
            <div class="card-body">
              <h5 class="card-title">Project start date</h5>
              <div class="mb-3">
                <label for="from" class="form-label">From</label>
                <input v-model="fromDate" type="date" class="form-control" id="from" aria-describedby="from">
              </div>
              <div class="mb-3">
                <label for="to" class="form-label">To</label>
                <input v-model="toDate" type="date" class="form-control" id="to" aria-describedby="to">
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="card mt-2">
        <div class="card-body">
          <h5 class="card-title mb-2">Priority</h5>
          <select v-model="priority" id="priority" class="form-control">
            <option v-for="option of priorityOptions" :selected="'true'">{{ option.priorityName }}</option>
          </select>
        </div>
      </div>

      <div class="card mt-2">
        <div class="card-body">
          <h5 class="card-title mb-2">Sort by</h5>
          <select v-model="sortBy" id="sortBy" class="form-control">
            <option v-for="option of sortByOptions" :selected="'true'">{{ option.sortName }}</option>
          </select>
        </div>
      </div>

      <div class="card mt-2">
        <div class="card-body">
          <h5 class="card-title">Sort</h5>
          <div class="btn-group" role="group" aria-label="Basic radio toggle button group">
            <input v-model="sort" :value="'none'" type="radio" class="btn-check" name="sort" id="sort1" autocomplete="off"
              checked>
            <label class="btn btn-outline-primary" for="sort1">none</label>

            <input v-model="sort" :value="'ascend'" type="radio" class="btn-check" name="sort" id="sort2">
            <label class="btn btn-outline-primary" for="sort2">ascend</label>

            <input v-model="sort" :value="'descent'" type="radio" class="btn-check" name="sort" id="sort3">
            <label class="btn btn-outline-primary" for="sort3">descent</label>
          </div>
        </div>
      </div>

      <button @click="filter" type="button" class="btn btn-primary">Apply</button>
    </div>
  </div>
</template>

<script>
import EventBus from '@/helpers/EventBus'
import {
  projectsFiltered,
  toggleProjectFilterSidebar,
} from '@/helpers/variables'

export default {
  data() {
    return {
      isActive: false,
      fromDate: null,
      toDate: null,
      priority: null,
      sort: 'none',
      sortBy: 'name',
      filteredProjects: null,
      sortByOptions: {
        'name': {
          sortName: 'Name',
        },
        'date': {
          sortName: 'Date',
        },
        'priority': {
          sortName: 'Priority',
        }
      },
      priorityOptions: {
        'None': {
          priorityName: 'None',
          priority: 0
        },
        'Medium': {
          priorityName: 'Medium',
          priority: 1
        },
        'High': {
          priorityName: 'High',
          priority: 2
        }
      }
    }
  },
  mounted() {
    EventBus.on(toggleProjectFilterSidebar, this.toggle)
  },
  methods: {
    filter() {
      if (this.filteredProjects == null) return

      if (this.fromDate != null && this.toDate != null) {
        let date;
        this.filteredProjects = this.filteredProjects?.filter(project => {
          date = project.projectStartDate
          if (date >= this.fromDate && date <= this.toDate) {
            return project
          }
        })
      }

      if (this.priority != null) {
        let priority = this.priorityOptions[this.priority].priority
        this.filteredProjects = this.filteredProjects?.filter(project => {
          return project.priority == priority
        })
      }

      if (this.sort != 'none') {
        switch (this.sortBy) {
          case 'Name':
            this.sortByName()
            break;
          case 'Date':
            this.sortByDate()
            break;
          case 'Priority':
            this.sortByPriority()
            break;
        }

        if (this.sort == 'ascend')
          this.filteredProjects?.reverse()
      }

      EventBus.trigger(projectsFiltered, this.filteredProjects)
      this.toggle()
    },
    sortByName() {
      this.filteredProjects = this.filteredProjects
        ?.sort((a, b) => a.name.localeCompare(b.name))
    },
    sortByPriority() {
      this.filteredProjects = this.filteredProjects
        ?.sort((a, b) => a.priority - b.priority)
    },
    sortByDate() {
      this.filteredProjects = this.filteredProjects?.sort((a, b) => {
        if (a.projectStartDate < b.projectStartDate) return 1
        if (a.projectStartDate > b.projectStartDate) return -1
        return 0
        })
    },
    toggle(projects) {
      this.isActive = !this.isActive
      this.filteredProjects = projects
    }
  }
}
</script>