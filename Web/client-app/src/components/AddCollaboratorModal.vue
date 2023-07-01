<template>
  <div v-if="isActive" class="modal fade show" tabindex="-1" aria-labelledby="gridModalLabel"
       style="display: block; padding-left: 0;" aria-modal="true" role="dialog">
    <div class="modal-dialog modal-dialog-scrollable">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="gridModalLabel">Grids in modals</h1>
          <button type="button" @click="toggle" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <div class="container-fluid bd-example-row">
            <div class="row row-cols-1 align-items-center">
              <div v-for="collaborator in collaborators" class="card">
                <div v-if="collaborator != null" class="card-body">
                  <h5 class="card-title">{{ collaborator.firstName }} {{ collaborator.lastName }} {{
                    collaborator.patronymic }}</h5>
                  <p class="card-text">{{ collaborator.email }}</p>
                  <button v-if="selectedCollaborators != null && isCollaboratorSelected(collaborator)"
                    @click.prevent="deleteCollaborator(collaborator.id)" class="btn btn-danger">Delete</button>
                  <button v-else @click.prevent="addCollaborator(collaborator)" class="btn btn-primary">Add</button>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="modal-footer">
          <button @click="toggle" type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
          <button @click="saveChanges" type="button" class="btn btn-primary">Save changes</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import EventBus from '@/helpers/EventBus';
import { getAll } from '@/services/collaboratorRequestService';
import {
  changeSelectedCollaborators,
  toggleAddCollaboratorModal,
  saveSelectedCollaboratorList
} from '@/helpers/variables';

export default {
  data() {
    return {
      isActive: false,
      collaborators: [],
      selectedCollaborators: {}
    }
  },
  created() {
    getAll()
      .then(data => this.collaborators = data)
  },
  mounted() {
    EventBus.on(toggleAddCollaboratorModal, this.toggle)
    EventBus.on(changeSelectedCollaborators, this.changeSelectedCollaborators)
  },
  methods: {
    toggle() {
      this.isActive = !this.isActive
    },
    addCollaborator(collaborator) {
      this.selectedCollaborators[collaborator.id] = collaborator
    },
    deleteCollaborator(id) {
      delete this.selectedCollaborators[id]      
    },
    saveChanges() {
      EventBus.trigger(saveSelectedCollaboratorList, Object.assign({}, this.selectedCollaborators))
    },
    changeSelectedCollaborators(collaborators) {
      this.selectedCollaborators = {}
      Object.values(collaborators).forEach(collaborator => {
        this.selectedCollaborators[collaborator.id] = collaborator
      })
    },
    isCollaboratorSelected(collaborator) {
      return Object.values(this.selectedCollaborators)
        .find(selectedCollaborator => selectedCollaborator.id === collaborator.id) != null
    }
  }
}
</script>