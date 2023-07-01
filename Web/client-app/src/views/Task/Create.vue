<template>
  <form v-on:submit.prevent="create">
    <div class="mb-3">
      <label for="name" class="form-label">Name</label>
      <input type="text" v-model="task.name" class="form-control" id="name" aria-describedby="name">
    </div>

    <div class="mb-3">
      <label for="status" class="form-label">Status</label>
      <select v-model="rawData.status" id="status" class="form-control">
        <option v-for="option of statusOptions" :selected="rawData.status">{{ option.statusName }}</option>
      </select>i
    </div>

    <div class="mb-3">
      <label for="priority" class="form-label">Priority</label>
      <select v-model="rawData.priority" id="priority" class="form-control">
        <option v-for="option of priorityOptions" :selected="rawData.priority">{{ option.priorityName }}</option>
      </select>
    </div>

    <div class="mb-3">
      <label for="comment" class="form-label">Comment</label>
      <textarea type="text" v-model="task.comment" class="form-control" id="comment" aria-describedby="comment"
        placeholder="Type comment"></textarea>
    </div>

    <div class="mb-3">
      <label class="form-label">Author</label>
      <div v-if="task.author != null" class="m-3 d-flex justify-content-center">
        <div class="card" style="width: 18rem;">
          <div class="card-body">
            <h6 class="card-title">{{ task.author.firstName }} {{ task.author.lastName }}
              {{task.author.patronymic }}
            </h6>
            <p class="card-text">{{ task.author.email }}</p>
          </div>
        </div>
      </div>
      <SetCollaboratorModal :collaborator="task.author" :collaborators="collaborators" @change="setTaskAuthor" />
    </div>

    <div class="mb-3">
      <label class="form-label">Performer</label>
      <div v-if="task.performer != null" class="m-3 d-flex justify-content-center">
        <div class="card" style="width: 18rem;">
          <div class="card-body">
            <h6 class="card-title">{{ task.performer.firstName }} {{ task.performer.lastName }}
              {{task.performer.patronymic }}
            </h6>
            <p class="card-text">{{ task.performer.email }}</p>
          </div>
        </div>
      </div>
      <SetCollaboratorModal :collaborator="task.performer" :collaborators="collaborators" @change="setTaskPerformer" />
    </div>

    <button type="submit" class="btn btn-primary">Create</button>
  </form>
</template>

<script>
import { create } from '@/services/taskRequestService.js'
import { getAll } from '@/services/collaboratorRequestService'
import SetCollaboratorModal from '@/components/SetCollaboratorModal.vue'

export default {
  data() {
    return {
      task: {
        name: null,
        author: null,
        performer: null,
        comment: null,
        status: null,
        priority: null
      },
      collaborators: null,
      rawData: {
        status: 'ToDo',
        priority: 'None',
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
  created() {
    getAll()
      .then(data => this.collaborators = data)
  },
  components: {
    SetCollaboratorModal
  },
  methods: {
    create() {
      this.task.priority = this.priorityOptions[this.rawData.priority].priority
      this.task.status = this.statusOptions[this.rawData.status].status

      console.log(JSON.stringify(this.task));
      create(this.task)
      this.$router.push('/task')
    },
    setTaskAuthor(collaborator) {
      console.log('author: '+ collaborator)
      this.task.author = collaborator
    },
    setTaskPerformer(collaborator) {
      console.log('performer: ' + collaborator)
      this.task.performer = collaborator
    }
  }
}
</script>