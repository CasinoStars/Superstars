<template>
<div>
    <img src="../img/back.png" id="deck">
</div>
</template>

<script>
import { mapActions } from 'vuex';
import BlackJackApiService from '../services/BlackJackApiService';
import Vue from 'vue';


export default {
    data() {
        return {

        }
    },

    methods: {
        ...mapActions(['executeAsyncRequest']),
    }
}
</script>

<style>
 #deck {
     height: 215px;
     width: 135px;
 }
</style>



