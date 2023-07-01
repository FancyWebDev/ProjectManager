<template>
  <StepOne
      v-if="currentStep === 1"
      v-model:name="project.name"
      v-model:projectStartDate="project.projectStartDate"
      v-model:projectEndDate="project.projectEndDate"
      v-model:priority="project.priority"
  />
  <StepTwo
      v-if="currentStep === 2"
      v-model:customerCompanyName="project.customerCompanyName"
      v-model:performerCompanyName="project.performerCompanyName"
  />
  <StepThree
      v-if="currentStep === 3"
      v-model:collaborators="rawData.collaborators"
      @deleteCollaborator="deleteCollaborator"
      @updateCollaborators="updateCollaboratorList"
  />
  <StepFour
      v-if="currentStep === 4"
      v-model:director="rawData.director"
      v-model:collaborators="rawData.collaborators"
      @change="setDirector"
  />
  <StepFive
      v-if="currentStep === 5"
      :files="rawData.files"
      @addFiles="addFiles"
      @removeFile="removeFile"
  />
  <div class="container">
    <button
        :class="currentStep === minStep ? 'disabled' : ''"
        @click="previous"
        type="button" class="btn btn-primary">
      Previous
    </button>
    <button
        v-if="currentStep < maxStep"
        @click="next"
        type="button" class="btn btn-primary">
      Next
    </button>
    <button
        v-if="currentStep === 5"
        @click="createProject"
        type="button" class="btn btn-primary">
      Create
    </button>
  </div>
</template>

<script setup>
import StepOne from '@/views/Project/Wizard/StepOne'
import StepTwo from '@/views/Project/Wizard/StepTwo'
import StepThree from '@/views/Project/Wizard/StepThree'
import StepFour from '@/views/Project/Wizard/StepFour'
import StepFive from '@/views/Project/Wizard/StepFive'
import { reactive, ref } from 'vue'
import { useRouter } from 'vue-router'
import { create } from '@/services/projectRequestService'

const router = useRouter()

const project = reactive({
  name: null,
  customerCompanyName: null,
  performerCompanyName: null,
  directorId: null,
  collaborators: [],
  priority: 0,
  projectStartDate: null,
  projectEndDate: null,
  files: []
})

const rawData = reactive({
  director: null,
  collaborators: {},
  files: []
})

const minStep = ref(1)
const maxStep = ref(5)
let currentStep = ref(1)

function next() {
  currentStep.value++
}

function previous() {
  currentStep.value--
}

function createProject() {
  project.collaborators = Object.values(rawData.collaborators)
  project.directorId = rawData.director.id

  const files = rawData.files.map(({ file }) => file)
  project.files = files

  create(project)
  router.push('/project')
}

function updateCollaboratorList(collaborators) {
  rawData.collaborators = collaborators
  if (rawData.director == null) return

  const director = Object.values(collaborators)
                         .find(collaborator => collaborator.id === rawData.director.id)

  if (director == null)
    delete rawData.director
}

function deleteCollaborator(collaborator) {
  delete rawData.collaborators[collaborator.id]

  if (rawData.director == null) return

  if (collaborator.id === rawData.director.id)
    delete rawData.director
}

function setDirector(collaborator) {
  rawData.director = collaborator
}

function addFiles(newFiles) {
  let newUploadableFiles = [...newFiles]
      .map(file => new UploadableFile(file))
      .filter(file => !fileExists(file.id))
  rawData.files = rawData.files.concat(newUploadableFiles)
}

function fileExists(otherId) {
  return rawData.files.some(({ id }) => id === otherId)
}

function removeFile(file) {
  const index = rawData.files.indexOf(file)

  if (index > -1) rawData.files.splice(index, 1)
}

class UploadableFile {
  constructor(file) {
    this.file = file
    this.id = `${file.name}-${file.size}-${file.lastModified}-${file.type}`
    this.url = URL.createObjectURL(file)
    this.status = null
  }
}
</script>