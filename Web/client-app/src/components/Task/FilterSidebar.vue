<template>
  <div v-if="isActive" class="offcanvas offcanvas-end show" tabindex="-1" id="offcanvasRight"
    aria-labelledby="offcanvasRightLabel" aria-modal="true" role="dialog">
    <div class="offcanvas-header">
      <h5 class="offcanvas-title" id="offcanvasRightLabel">Offcanvas right</h5>
      <button @click="toggle" type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
    </div>
    <div class="offcanvas-body">
      <div class="card mt-2">
        <div class="card-body">
          <h5 class="card-title mb-2">Status</h5>
          <select v-model="status" id="priority" class="form-control">
            <option v-for="option of statusOptions" :selected="'true'">{{ option.statusName }}</option>
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

export default {
  data() {
    return {
      isActive: false,
      status: null,
      sort: 'none',
      sortBy: null,
      filteredTasks: null,
      sortByOptions: {
        'status': {
          sortName: 'Status',
        },
        'priority': {
          sortName: 'Priority',
        },
      },
      statusOptions: {
        'ToDo': {
          statusName: 'ToDo',
          status: 0
        },
        'InProgress': {
          statusName: 'InProgress',
          status: 1
        },
        'Done': {
          statusName: 'Done',
          status: 2
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
    EventBus.on('toggleTaskFilterSidebar', this.toggle)
  },
  methods: {
    toggle(tasks) {
      this.filteredTasks = tasks
      this.isActive = !this.isActive
    },
    filter() {
      if (this.status != null) {
        this.filteredTasks = this.filteredTasks.filter(task => {
          if (task.status == this.statusOptions[this.status].status)
            return task
        })
      }

      if (this.priority != null) {
        let priority = this.priorityOptions[this.priority].priority
        this.filteredTasks = this.filteredTasks?.filter(task => {
          return task.priority == priority
        })
      }

      if (this.sort != 'none') {
        switch (this.sortBy) {
          case 'Status':
            this.sortByStatus()
            break;
          case 'Priority':
            this.sortByPriority()
            break;
        }

        if (this.sort == 'ascend')
          this.filteredTasks?.reverse()
      }
    },
    sortByPriority() {
      this.filteredTasks = this.filteredTasks
        ?.sort((a, b) => a.priority - b.priority)
    },
    sortByStatus() {
      this.filteredTasks = this.filteredTasks
        ?.sort((a, b) => a.status - b.status)
    }
  }
}
</script>