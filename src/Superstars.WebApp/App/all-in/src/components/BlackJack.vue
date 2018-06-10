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
            playercards: [],
        }
    },

  async mounted() {
    await this.initplayer();
    await this.refreshCards();
  },

    methods: {
        ...mapActions(['executeAsyncRequest']),

    async initplayer() {
        await this.executeAsyncRequest(() => BlackJackApiService.InitPlayer());
    },

    async refreshCards() {
      this.playercards = await this.executeAsyncRequest(() => BlackJackApiService.GetPlayerCards());
      //this.iadices = await this.executeAsyncRequest(() => YamsApiService.GetIaDices());
    },

    }
}
</script>

<style>
 #deck {
     height: 215px;
     width: 135px;
 }
</style>



