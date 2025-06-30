<script setup lang="ts">
import { useRouter } from 'vue-router';

const router = useRouter();

function goToDiary() {
  router.push('/diary')
}

function goToTodo() {
  router.push('/todo')
}
</script>

<template>
    <ul class="menu">
        <li>
            <button @click="goToDiary">Dagbok</button>
        </li>
        <li>
            <button @click="goToTodo">Att göra</button>
        </li>
        <li>
            <button>Uppgifter</button>
        </li>
    </ul>
</template>

<style scoped>

.menu {
    display: flex;
    flex-direction: column;
    gap: 1em;
}

li{
    list-style: none;
}

button {
    padding: 2;
    background: #e9e9e9;
    width: 6.5em;
}
</style>