<script setup lang="ts">
const props = defineProps<{ postId: number }>();
const emit = defineEmits(['deleted']);

async function deletePost() {
    if (!confirm('Vill du verkligen ta bort detta inlägg?')) return;
    const response = await fetch(`http://localhost:5227/api/posts/${props.postId}`, {
        method: 'DELETE'
    });
    if (response.ok) {
        emit('deleted');
    } else {
        alert('Kunde inte ta bort inlägget.');
    }
}
</script>

<template>
    <button @click="deletePost" class="delete-btn"><i class="fa-solid fa-trash"></i></button>
</template>

<style scoped>

</style>