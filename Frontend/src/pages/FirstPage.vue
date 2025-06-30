<script setup lang="ts">
import Header from '../components/Header.vue'
import JumpInBtn from '../components/JumpInBtn.vue'
import Menu from '../components/Menu.vue'
import { ref, onMounted } from 'vue';

const moved = ref(false);
const showMenu = ref(false);

onMounted(() => {
    if (sessionStorage.getItem('usingPage') === 'true') {
        moved.value = true
        showMenu.value = true
    }
})

function moveHeader() {
    moved.value = true
}

function onHeaderTransitionEnd() {
    if (moved.value) {
        showMenu.value = true
    }
}

function getHeartStyle(n: number) {
    const left = Math.random() * 100;
    const size = 18 + Math.random() * 18;
    const duration = 4 + Math.random() * 4;
    const delay = Math.random() * 3;
    return {
        left: `${left}%`,
        width: `${size}px`,
        height: `${size}px`,
        animationDuration: `${duration}s`,
        animationDelay: `${delay}s`
    };
}
</script>

<template>
    <div v-if="!moved" class="cute-bg">
        <div v-for="n in 15" :key="n" class="heart" :style="getHeartStyle(n)"></div>
    </div>
    <Header :class="{ moved: moved, 'header-anim': true }" @transitionend="onHeaderTransitionEnd" />
    <JumpInBtn class="jumpInBtn" v-if="!moved" @jump-in="moveHeader" />
    <div v-if="showMenu">
        <Menu class="menu" />
    </div>
    <div v-if="moved">
        <p>a nice clock or smth</p>
        some tiny animations maybe
        <p>show small todolist?</p>
    </div>
</template>

<style scoped>
.header-anim {
    position: absolute;
    left: 50%;
    top: 35%;
    transform: translate(-50%, -35%);
    transition:
        left 1.3s cubic-bezier(.50, 0, .18, 1),
        top 1.3s cubic-bezier(.50, 0, .18, 1),
        transform 1.3s cubic-bezier(.50, 0, .18, 1),
        font-size 1.3s cubic-bezier(.50, 0, .18, 1);
}

.moved {
    left: -6em;
    top: 1em;
    transform: translate(0, 0);
    margin: 1rem;
    font-size: 3rem;
}

.jumpInBtn {
    margin-top: 8em;
}

.menu {
    position: absolute;
    top: 10rem;
    left: 9rem;
    z-index: 5;
    padding: 2rem 1rem;
}

.cute-bg {
    position: fixed;
    inset: 0;
    z-index: 0;
    pointer-events: none;
    overflow: hidden;
    width: 120vw;
}

.heart {
    position: absolute;
    bottom: -40px;
    left: 0;
    width: 32px;
    height: 32px;
    background: transparent;
    pointer-events: none;
    animation: floatUp 6s linear infinite;
    opacity: 0.8;
}
.heart::before,
.heart::after {
    content: '';
    position: absolute;
    background: #f8b6d8;
    border-radius: 50%;
}
.heart::before {
    width: 100%;
    height: 100%;
    left: 50%;
    top: 0;
    transform: translateX(-50%);
}
.heart::after {
    width: 100%;
    height: 100%;
    left: 0;
    top: -50%;
    transform: translateY(50%);
}
@keyframes floatUp {
    0% {
        transform: translateY(0) scale(1) rotate(-45deg);
        opacity: 0.7;
    }
    80% {
        opacity: 0.7;
    }
    100% {
        transform: translateY(-110vh) scale(1.1) rotate(-45deg);
        opacity: 0;
    }
}

</style>