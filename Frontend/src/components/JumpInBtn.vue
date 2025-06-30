<script setup lang="ts">
const emit = defineEmits(['jump-in'])

function jumpIn() {
    emit('jump-in');
    sessionStorage.setItem('usingPage', 'true')
}
</script>

<template>
    <button @click="jumpIn">Hoppa in</button>
</template>

<style scoped>
button{
    background: #e9e9e9;
}
</style>