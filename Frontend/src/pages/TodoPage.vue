<script setup lang="ts">
import TodoHeader from '../components/Todo/TodoHeader.vue'
import Header from '../components/Header.vue'
</script>

<template>
    <Header class="header" />
    <TodoHeader />
</template>

<style scoped>
.header {
    position: absolute;
    left: -6em;
    top: 1em;
    margin: 1rem;
    font-size: 3rem;
    z-index: 10;
}
</style>