<template>
  <nav class="navbar navbar-expand-md bg-dark sticky-top border-bottom" data-bs-theme="dark">
    <div class="container">
      <div class="offcanvas" tabindex="-1" id="#offcanvas" aria-labelledby="#offcanvasLabel">
        <div class="offcanvas-body">
          <ul class="navbar-nav d-flex flex-grow-1 p-2">
            <form class="d-flex" role="search">
              <input v-model="term"  @keyup="search" class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
            </form>
          </ul>
        </div>
      </div>
    </div>
  </nav>
</template>

<script>
import EventBus from '@/helpers/EventBus';
import { getAll } from '@/services/collaboratorRequestService';

export default {
  data() {
    return {
      term: '',
      collaborators: null,
      filteredCollaborators: {}
    }
  },
  created() {
    getAll()
      .then(data => this.collaborators = data)
  },
  mounted() {
    this.$forceUpdate();
  },
  methods: {
    search() {
      this.filteredCollaborators = this.collaborators.filter(({firstName, lastName, patronymic}) => {
        let fullName = firstName.concat(lastName, patronymic)
        return fullName.toLowerCase().includes(this.term.toLowerCase())
      })
      console.log(this.filteredCollaborators)
      EventBus.trigger('collaboratorsFiltered', this.filteredCollaborators)
    },
    toggleFilter() {

    }
  }
}
</script>