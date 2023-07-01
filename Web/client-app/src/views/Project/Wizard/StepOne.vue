<template>
  <div class="container mt-3">
    <div class="mb-3">
      <label for="name" class="form-label">Name</label>
      <input :value="name"
             @input="$emit('update:name', $event.target.value)"
             type="text" class="form-control" id="name" aria-describedby="name">
    </div>
    <div class="mb-3">
      <label for="start-date" class="form-label">Start Date</label>
      <input :value="projectStartDate"
             @input="$emit('update:project-start-date', $event.target.value)"
             type="date" class="form-control" id="start-date" aria-describedby="end date">
    </div>
    <div class="mb-3">
      <label for="end-date" class="form-label">End Date</label>
      <input :value="projectEndDate"
             @input="$emit('update:project-end-date', $event.target.value)" type="date" class="form-control"
             id="end-date" aria-describedby="end date">
    </div>
    <div class="mb-3">
      <label for="priority" class="form-label">Priority</label>
      <select :value="defaultPriority" @change="updatePriority"
              id="priority" class="form-control">
        <option v-for="option of priorityOptions">{{ option.priorityName }}</option>
      </select>
    </div>
  </div>
</template>

<script setup>
const defaultPriority = 'None'

const priorityOptions = {
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

const emit = defineEmits([
  'update:name',
  'update:project-start-date',
  'update:project-end-date',
  'update:priority'
])

defineProps({
  name: String,
  projectStartDate: Date,
  projectEndDate: Date,
  priority: Number
})

function updatePriority(event) {
  let priority = priorityOptions[event.target.value].priority
  emit('update:priority', priority)
}
</script>