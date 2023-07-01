<template>
  <div class="container mt-3">
    <div class="mb-3">
      <label class="form-label">Director</label>
      <SetCollaboratorModal :collaborator="director" :collaborators="collaborators" @change="setDirector"/>
      <div v-if="director != null" class="m-3 d-flex justify-content-center">
        <div class="card" style="width: 18rem;">
          <div class="card-body">
            <h6 class="card-title">{{ director.firstName }} {{ director.lastName }}
              {{ director.patronymic }}
            </h6>
            <p class="card-text">{{ director.email }}</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import SetCollaboratorModal from '@/components/SetCollaboratorModal'

defineProps({
  director: Object,
  collaborators: Object
})

const emit = defineEmits([
  'change',
  'update:director'
])

function setDirector(director) {
  emit('change', director)
}
</script>