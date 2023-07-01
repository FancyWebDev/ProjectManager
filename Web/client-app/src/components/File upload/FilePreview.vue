<template>
  <component :is="tag" class="file-preview">
    <button type="button" @click="$emit('remove', file)" class="btn-close" data-bs-dismiss="modal" aria-label="Remove"></button>
    <img :src="file.url" :alt="file.file.name" :title="file.file.name"/>
  </component>
</template>

<script setup>
defineProps({
  file: { type: Object, required: true },
  tag: { type: String, default: 'li' },
})

defineEmits(['remove'])
</script>