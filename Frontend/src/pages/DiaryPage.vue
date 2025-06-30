<script setup lang="ts">
import { ref } from 'vue'
import Header from '../components/Header.vue'
import DiaryHeader from '../components/Diary/DiaryHeader.vue'
import AddToDiary from '../components/Diary/Add.vue'
import AllPosts from '../components/Diary/All.vue';

// To refresh list of posts after adding new
const refreshKey = ref(0)
function refreshPosts() {
  refreshKey.value++
}
</script>

<template>
    <Header class="header" />
    <DiaryHeader />
    <AddToDiary @post-added="refreshPosts"  />
    <AllPosts :refresh="refreshKey" />
</template>

<style scoped>
.header {
    position: fixed;
    left: -6em;
    top: 1em;
    margin: 1rem;
    font-size: 3rem;
    z-index: 10;
}
</style>