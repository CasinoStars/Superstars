<template>
<div>
    <img src="../../../../wwwroot/images/back.png" id="deck"/>
   <div v-for="(x, index) in playercards" :key="index">
       <img v-bind:src="'../../../../wwwroot/images/' + playercards[0] +  playercards[1] + '.png'" id="firstcard"/>
   </div>
   <a> {{nbturn}}</a>

   <form @submit="hitorstand($event)">
   <div style="text-align:center;"><button type="submit" value="hit" class="btn btn-outline-secondary btn-lg" >HIT</button></div>
   <div style="text-align:center;"><button type="submit" value="stand" class="btn btn-outline-secondary btn-lg" >STAND</button></div>
   </form>
   
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
            nbturn: 0,
        }
    },

  async mounted() {
    this.nbturn = await this.executeAsyncRequest(() => BlackJackApiService.GetTurn());
    await this.initplayer();
    await this.refreshCards();

  },

    methods: {
        ...mapActions(['executeAsyncRequest']),

    async initplayer() {
        if(this.nbturn == 0) {
        await this.executeAsyncRequest(() => BlackJackApiService.InitPlayer());
        }
    },

    async refreshCards() {
      this.playercards = await this.executeAsyncRequest(() => BlackJackApiService.GetPlayerCards());
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



