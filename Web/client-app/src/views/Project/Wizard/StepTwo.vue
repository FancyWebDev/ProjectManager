<template>
  <div class="container mt-3">
    <div class="mb-3">
      <label for="customer-company-name" class="form-label">Customer Company Name</label>
      <input :value="customerCompanyName"
             @change="$emit('update:customerCompanyName', $event.target.value)"
             type="text" class="form-control" id="customer-company-name" aria-describedby="customer company name">
    </div>
    <div class="mb-3">
      <label for="performer-company-name" class="form-label">Performer Company Name</label>
      <input :value="performerCompanyName"
             @change="$emit('update:performerCompanyName', $event.target.value)"
             type="text" class="form-control" id="performer-company-name" aria-describedby="performer company name">
    </div>
  </div>
</template>

<script setup>
defineProps({
  customerCompanyName: String,
  performerCompanyName: String
})

defineEmits([
  'update:customerCompanyName',
  'update:performerCompanyName'
])
</script>