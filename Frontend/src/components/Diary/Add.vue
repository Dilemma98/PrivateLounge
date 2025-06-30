<script setup lang="ts">
import { ref } from 'vue';
const emit = defineEmits(['post-added']);
const post = ref({
    title:'',
    content: '',
    image: null as File | null
});

// To handle image uploads, we can use a File object
const images = ref<File[]>([]);

function handleImageUpload(event: Event) {
  const target = event.target as HTMLInputElement;
  images.value = target.files ? Array.from(target.files) : [];
}

// Function to submit the post
async function submitPost() {
  const formData = new FormData();
  formData.append('title', post.value.title);
  formData.append('content', post.value.content);
  // Lägg till alla bilder i formData
  images.value.forEach(img => formData.append('images', img));

  try {
    const response = await fetch('http://localhost:5227/api/posts', {
      method: 'POST',
      body: formData
    });

    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    // Reset the form after submission
    post.value.title = '';
    post.value.content = '';
    images.value = [];

   emit('post-added')
  }
  catch (error) {
    console.error('Error submitting post:', error);
  }
}
</script>

<template>
    <form @submit.prevent="submitPost" class="post-form">
      <div class="form-group">
        <input type="text" id="title" v-model="post.title" required class="form-input" placeholder="Titel" />
      </div>
      <div class="form-group">
        <textarea id="content" v-model="post.content" required class="form-textarea" placeholder="Innehåll"></textarea>
      </div>
      <div class="form-group">
        <!-- <label for="image" class="form-label">Bild:</label> -->
        <input type="file" id="image" @change="handleImageUpload" class="form-input-file" multiple />
      </div>
      <button type="submit" class="form-button">Skicka in</button>
    </form>
</template>

<style scoped>
.post-form {
    position: fixed;
    top: 10rem;
    left: 5rem;
    z-index: 5;
    padding: 2rem 2.5rem;
    width:15%;
}

.form-group {
    margin-bottom: 1.5rem;
}

.form-input,
.form-textarea {
    width: 100%;
    padding: 0.75rem 1rem;
    border: 1.5px solid #bfc9d1;
    border-radius: 0.5rem;
    font-size: 0.9rem;
    background: #f8fafc;
    transition: border-color 0.2s;
    outline: none;
    box-sizing: border-box;
}

.form-input:focus,
.form-textarea:focus {
    border-color: #3b82f6;
    background: #fff;
}

.form-textarea {
    min-height: 100px;
    resize: vertical, horizontal;
}

.form-input-file {
    border: none;
    background: none;
    font-size: 1rem;
    margin-top: 0.5rem;
}

.form-button {
    width: 100%;
    padding: 0.8rem 0;
    background: #e4a6ca;
    color: #fff;
    border: none;
    border-radius: 0.5rem;
    font-size: 1.1rem;
    font-weight: 600;
    cursor: pointer;
    transition: background 0.2s;
    box-shadow: 0 2px 8px rgba(59,130,246,0.08);
}

.form-button:hover {
    background: #c386aa;
}
</style>