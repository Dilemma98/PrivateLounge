<script setup lang="ts">
import {useRouter} from 'vue-router'
const router = useRouter();

function goToHome(){
    router.push("/");
}

</script>

<template>
    <h1><button @click="goToHome" >Välkommen Emma!</button></h1>
</template>

<style scoped>
@import url('https://fonts.cdnfonts.com/css/boundless');
h1{
    width:70vw;
    font-family: 'Boundless';
    font-size: 4.5em;
}
button{
    padding: 0;
    background: transparent;
}
button:hover{
    border: none;   
}

</style>