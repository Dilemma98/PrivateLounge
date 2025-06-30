<script setup lang="ts">
import { ref, onMounted, watch } from 'vue';
import Delete from './Delete.vue';

const props = defineProps<{ refresh: number }>()

const openImagePostId = ref<number | null>(null);
const openImageImageIdx = ref<number | null>(null);

function openImage(postId: number, imageIdx: number) {
    openImagePostId.value = postId;
    openImageImageIdx.value = imageIdx;
}
function closeImage() {
    openImagePostId.value = null;
    openImageImageIdx.value = null;
}

// Type for a Post object
interface Post {
    id: number;
    title: string;
    content: string;
    imageUrl: string | null; // URL to the image, or null if no image
    createdAt: string; // ISO date string
}

// Reactive variable to store posts
const posts = ref<Post[]>([]);

// Funktion för att hämta poster
async function fetchPosts() {
    try {
        const response = await fetch('http://localhost:5227/api/posts');
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        console.log("uppdaterar...")
        const data = await response.json();
        // Sortera poster efter datum
        posts.value = data.sort((a: Post, b: Post) =>
            new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime());
    } catch (error) {
        console.error('There has been a problem with your fetch operation:', error);
    }
}

// Kör när komponenten mountas
onMounted(fetchPosts);

// Kör när refresh ändras
watch(() => props.refresh, fetchPosts);

</script>

<template>
    <div class="posts-container">
        <ul>
            <!-- Loop through posts and display each one -->
            <li v-for="post in posts" :key="post.id">
                <div class="post">
                    <h3 class="title">{{ post.title }}</h3>
                    <p class="content">{{ post.content }}</p>
                    <div v-if="post.imageUrl"class="post-images">
                        <img v-for="(url, idx) in post.imageUrl.split(',')" :key="url"
                            :src="`http://localhost:5227${url}`" alt="Post image" class="post-image"
                            @click="openImage(post.id, idx)" />
                        <!-- Fullscreen-visning för vald bild -->
                        <div v-if="openImagePostId === post.id && openImageImageIdx !== null" class="image-overlay"
                            @click="closeImage">
                            <img :src="`http://localhost:5227${post.imageUrl.split(',')[openImageImageIdx]}`"
                                alt="Post image" class="fullscreen-image" />
                        </div>
                    </div>
                    <Delete :postId="post.id" @deleted="fetchPosts" class="edit-or-delete" />
                    <p class="datePosted" v-if="post.createdAt">
                        <strong>Postat:</strong>
                        {{
                            new Date(post.createdAt).toLocaleDateString('sv-SE')
                        }}
                        <br />
                        {{
                            new Date(post.createdAt).toLocaleTimeString('sv-SE', { hour: '2-digit', minute: '2-digit' })
                        }}
                    </p>

                </div>
            </li>
        </ul>
    </div>

</template>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Dancing+Script:wght@700&display=swap');

.posts-container {
    width: 60vw;
    height: 90vh;
    margin-left: 24rem;
    padding: 0 1rem;
}

.content {
    width: 80%;
    margin: auto;
    padding-top: 2em;
}

ul {
    list-style-type: none;
    width: 100%;
    background: none;
    margin: 0;
    padding: 0;
}

li {
    border: none;
    padding: 2rem 2.5rem 1.5rem 2.5rem;
    border-radius: 1rem;
    background: #fff;
    box-shadow: 0 4px 24px rgba(0, 0, 0, 0.08);
    transition: box-shadow 0.3s;
    margin: 2.5rem 0;
    position: relative;
}

li:hover {
    box-shadow: 0 8px 32px rgba(59, 130, 246, 0.10);
}

.title {
    font-family: 'Dancing Script', sans-serif;
    font-size: 2rem;
    color: #2d3748;
    text-align: center;
    line-height: 1.2;
    letter-spacing: 0.01em;
    border-bottom: 2px solid #afb0b3;
    width: 50%;
    margin: auto;
}

.post {
    font-family: 'Georgia', 'Times New Roman', Times, serif;
    font-size: 1.15rem;
    line-height: 1.8;
    color: #333;
    margin-bottom: 1.2rem;
}

.post-images {
    display: flex;
    flex-wrap: wrap;
    gap: 0rem;
    justify-content: center;
    margin-top: 1.5rem;
}

.post-image {
    max-width: 40%;
    height: auto;
    margin: 1.5rem auto 1rem auto;
    border-radius: 0.7rem;
    box-shadow: 0 2px 8px rgba(59, 130, 246, 0.08);
    cursor: pointer;
    transition: box-shadow 0.3s;
}

.post-image:hover {
    box-shadow: 0 4px 16px rgba(59, 130, 246, 0.15);
}

.image-overlay {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: rgba(0, 0, 0, 0.256);
    display: flex;
    align-items: center;
    justify-content: center;
    z-index: 1000;
}

.fullscreen-image {
    max-width: 95vw;
    max-height: 95vh;
    border-radius: 1rem;
    box-shadow: 0 8px 32px rgba(0, 0, 0, 0.4);
}

.datePosted {
    font-size: 0.95rem;
    color: #7b8794;
    font-style: italic;
    margin-top: 0.7rem;
    text-align: right;
    letter-spacing: 0.03em;
}

.edit-or-delete {
    width: 3.5em;
    display: flex;
    gap: 2rem;
    margin-top: 3rem;
}

@media (max-width: 700px) {
    .posts-container {
        max-width: 100vw;
        padding: 0 0.5rem;
    }

    li {
        padding: 1.2rem 0.7rem;
    }

    .title {
        font-size: 1.3rem;
    }
}
</style>