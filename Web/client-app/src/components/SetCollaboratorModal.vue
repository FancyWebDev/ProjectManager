<template>
  <button type="button" @click="toggle" class="btn btn-primary form-control">Select</button>
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
              <div v-for="collaborator in selectedCollaborators" class="card">
                <div class="card-body">
                  <h5 class="card-title">{{ collaborator.firstName }} {{ collaborator.lastName }}
                    {{collaborator.patronymic }}</h5>
                  <p class="card-text">{{ collaborator.email }}</p>
                  <button v-if="targetCollaborator == null || targetCollaborator.id !== collaborator.id"
                    @click.prevent="setCollaborator(collaborator)" class="btn btn-primary">Set collaborator</button>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="modal-footer">
          <button @click="toggle" type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
          <button @click="$emit('change', targetCollaborator)" type="button" class="btn btn-primary">Save changes</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import EventBus from '@/helpers/EventBus'
import {
  changeSelectedCollaborators,
  toggleSetCollaboratorModal
} from '@/helpers/variables'
export default {
  props: ['collaborator', 'collaborators'],
  emits: ['change'],
  data() {
    return {
      isActive: false,
      targetCollaborator: null,
      selectedCollaborators: {}
    }
  },
  mounted() {
    EventBus.on(toggleSetCollaboratorModal, this.toggle)
    EventBus.on(changeSelectedCollaborators, this.changeSelectedCollaborators)
  },
  methods: {
    setCollaborator(collaborator) {
      this.targetCollaborator = collaborator
    },
    toggle() {
      this.isActive = !this.isActive
      this.targetCollaborator = this.collaborator
      if(this.collaborators != null)
        this.selectedCollaborators = Object.assign({}, this.collaborators)
    },
    changeSelectedCollaborators(collaborators) {
      console.log(collaborators);
      this.selectedCollaborators = collaborators
    }
  }
}
</script>