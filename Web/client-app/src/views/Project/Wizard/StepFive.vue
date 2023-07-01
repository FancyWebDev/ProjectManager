<template>
  <div class="container">
    <DropZone class="drop-area" @files-dropped="addFiles" #default="{ dropZoneActive }">
      <label for="file-input">
        <span v-if="dropZoneActive">
            <span>Drop Them Here</span>
            <span class="smaller">to add them</span>
        </span>
        <span v-else>
          <span>Drag Your Files Here</span>
          <span class="smaller">
            or <strong><em>click here</em></strong> to select files
          </span>
        </span>
        <input type="file" id="file-input" name="files" multiple @change="onInputFiles"/>
      </label>
      <ul class="image-list" v-show="files.length">
        <FilePreview
            v-for="file of files"
            :key="file.id"
            :file="file"
            @remove="removeFile"
            tag="li"/>
      </ul>
    </DropZone>
  </div>
</template>

<script setup>
import DropZone from '@/components/File upload/DropZone'
import FilePreview from '@/components/File upload/FilePreview'

defineProps({
  files: Array
})

const emit = defineEmits([
  'add-files',
  'remove-file'
])

function addFiles(files) {
  emit('add-files', files)
}

function removeFile(file) {
  emit('remove-file', file)
}

function onInputFiles(event) {
  addFiles(event.target.files)
  event.target.value = null
}

</script>

<style>
html {
  height: 100%;
  width: 100%;
  background-color: #b6d6f5;

  /* Overlapping Stripes Background, based off https://codepen.io/MinzCode/pen/Exgpqpe */
  --color1: rgba(55, 165, 255, 0.35);
  --color2: rgba(96, 181, 250, 0.35);
  --rotation: 135deg;
  --size: 10px;
  background-blend-mode: multiply;
  background-image: repeating-linear-gradient(
      var(--rotation),
      var(--color1) calc(var(--size) * 0),
      var(--color1) calc(var(--size) * 9),
      var(--color2) calc(var(--size) * 9),
      var(--color2) calc(var(--size) * 12),
      var(--color1) calc(var(--size) * 12)
  ),
  repeating-linear-gradient(
      calc(var(--rotation) + 90deg),
      var(--color1) calc(var(--size) * 0),
      var(--color1) calc(var(--size) * 9),
      var(--color2) calc(var(--size) * 9),
      var(--color2) calc(var(--size) * 12),
      var(--color1) calc(var(--size) * 12)
  );
}

body {
  height: 100%;
  margin: 0;
  padding: 0;
}
</style>

<style>
.drop-area {
  width: 100%;
  max-width: 800px;
  margin: 0 auto;
  padding: 50px;
  background: #ffffff55;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
  transition: .2s ease;
}

#app {
  font-family: Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  height: 100%;
  display: flex;
  flex-direction: column;
}

[data-active=true] {
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.5);
  background: #ffffffcc;
}

label {
  font-size: 36px;
  cursor: pointer;
  display: block;
}

span {
  display: block;
}

input[type=file]:not(:focus-visible) {
  /*/ / Visually Hidden Styles taken from Bootstrap 5 position: absolute !important;*/
  width: 1px !important;
  height: 1px !important;
  padding: 0 !important;
  margin: -1px !important;
  overflow: hidden !important;
  clip: rect(0, 0, 0, 0) !important;
  white-space: nowrap !important;
  border: 0 !important;
}

.smaller {
  font-size: 16px;
}

.image-list {
  display: flex;
  list-style: none;
  flex-wrap: wrap;
  padding: 0;
}

.upload-button {
  display: block;
  appearance: none;
  border: 0;
  border-radius: 50px;
  padding: 0.75rem 3rem;
  margin: 1rem auto;
  font-size: 1.25rem;
  font-weight: bold;
  background: #369;
  color: #fff;
  text-transform: uppercase;
}

button {
  cursor: pointer;
}
</style>