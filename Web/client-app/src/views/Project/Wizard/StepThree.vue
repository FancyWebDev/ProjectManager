<template>
  <div class="container">
    <div class="mb-3">
      <label class="form-label">Collaborators</label>
      <button @click="toggle" type="button" class="btn btn-primary form-control">Add
        Collaborator
      </button>
      <div class="row row-cols-3 m-3 gap-3">
        <div v-for="collaborator in collaborators" class="card" style="width: 18rem;">
          <div class="card-body">
            <h6 class="card-title">{{ collaborator.firstName }} {{ collaborator.lastName }} {{ collaborator.patronymic }}
            </h6>
            <p class="card-text">{{ collaborator.email }}</p>
            <button @click.prevent="$emit('deleteCollaborator', collaborator)" class="btn btn-danger">Delete</button>
          </div>
        </div>
      </div>
      <AddCollaboratorModal/>
    </div>
  </div>
</template>

<script setup>
import AddCollaboratorModal from '@/components/AddCollaboratorModal'
import EventBus from '@/helpers/EventBus'
import { toggleAddCollaboratorModal, saveSelectedCollaboratorList, changeSelectedCollaborators } from '@/helpers/variables'
import { onMounted } from 'vue'

const props = defineProps({
  collaborators: Object
})

const emit = defineEmits([
  'deleteCollaborator',
  'updateCollaborators'
])

onMounted(() => {
  EventBus.on(saveSelectedCollaboratorList, updateCollaborators)
})

function updateCollaborators(collaborators) {
  emit('updateCollaborators', collaborators)
}

function toggle(){
  EventBus.trigger(changeSelectedCollaborators, Object.assign({}, props.collaborators))
  EventBus.trigger(toggleAddCollaboratorModal)
}
</script>