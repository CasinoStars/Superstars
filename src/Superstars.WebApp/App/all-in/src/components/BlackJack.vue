<template>
<div>
    <div v-for="(i, index) of playercards" :key="index" class="playercards">
      <img v-if="i == '2p'" src="../img/2p.png">
      <img v-if="i == '3p'" src="../img/3p.png">
      <img v-if="i == '4p'" src="../img/4p.png">
      <img v-if="i == '5p'" src="../img/5p.png">      
      <img v-if="i == '6p'" src="../img/6p.png">
      <img v-if="i == '7p'" src="../img/7p.png">      
      <img v-if="i == '8p'" src="../img/8p.png">
      <img v-if="i == '9p'" src="../img/9p.png">      
      <img v-if="i == '10p'" src="../img/10p.png">
      <img v-if="i == '11p'" src="../img/11p.png">      
      <img v-if="i == '12p'" src="../img/12p.png">
      <img v-if="i == '13p'" src="../img/13p.png">
      <img v-if="i == '14p'" src="../img/1p.png">

      <img v-if="i == '2h'" src="../img/2h.png">
      <img v-if="i == '3h'" src="../img/3h.png">
      <img v-if="i == '4h'" src="../img/4h.png">
      <img v-if="i == '5h'" src="../img/5h.png">      
      <img v-if="i == '6h'" src="../img/6h.png">
      <img v-if="i == '7h'" src="../img/7h.png">      
      <img v-if="i == '8h'" src="../img/8h.png">
      <img v-if="i == '9h'" src="../img/9h.png">      
      <img v-if="i == '10h'" src="../img/10h.png">
      <img v-if="i == '11h'" src="../img/11h.png">      
      <img v-if="i == '12h'" src="../img/12h.png">
      <img v-if="i == '13h'" src="../img/13h.png">
      <img v-if="i == '14h'" src="../img/1h.png">

      <img v-if="i == '2t'" src="../img/2t.png">
      <img v-if="i == '3t'" src="../img/3t.png">
      <img v-if="i == '4t'" src="../img/4t.png">
      <img v-if="i == '5t'" src="../img/5t.png">      
      <img v-if="i == '6t'" src="../img/6t.png">
      <img v-if="i == '7t'" src="../img/7t.png">      
      <img v-if="i == '8t'" src="../img/8t.png">
      <img v-if="i == '9t'" src="../img/9t.png">      
      <img v-if="i == '10t'" src="../img/10t.png">
      <img v-if="i == '11t'" src="../img/11t.png">      
      <img v-if="i == '12t'" src="../img/12t.png">
      <img v-if="i == '13t'" src="../img/13t.png">
      <img v-if="i == '14t'" src="../img/1t.png">

      <img v-if="i == '2c'" src="../img/2c.png">
      <img v-if="i == '3c'" src="../img/3c.png">
      <img v-if="i == '4c'" src="../img/4c.png">
      <img v-if="i == '5c'" src="../img/5c.png">      
      <img v-if="i == '6c'" src="../img/6c.png">
      <img v-if="i == '7c'" src="../img/7c.png">      
      <img v-if="i == '8c'" src="../img/8c.png">
      <img v-if="i == '9c'" src="../img/9c.png">      
      <img v-if="i == '10c'" src="../img/10c.png">
      <img v-if="i == '11c'" src="../img/11c.png">      
      <img v-if="i == '12c'" src="../img/12c.png">
      <img v-if="i == '13c'" src="../img/13c.png">
      <img v-if="i == '14c'" src="../img/1c.png">

    </div>

    <div v-for="(i, index) of dealercards" :key="index" class="dealercards">
      <img v-if="i == '2p'" src="../img/2p.png">
      <img v-if="i == '3p'" src="../img/3p.png">
      <img v-if="i == '4p'" src="../img/4p.png">
      <img v-if="i == '5p'" src="../img/5p.png">      
      <img v-if="i == '6p'" src="../img/6p.png">
      <img v-if="i == '7p'" src="../img/7p.png">      
      <img v-if="i == '8p'" src="../img/8p.png">
      <img v-if="i == '9p'" src="../img/9p.png">      
      <img v-if="i == '10p'" src="../img/10p.png">
      <img v-if="i == '11p'" src="../img/11p.png">      
      <img v-if="i == '12p'" src="../img/12p.png">
      <img v-if="i == '13p'" src="../img/13p.png">
      <img v-if="i == '14p'" src="../img/1p.png">

      <img v-if="i == '2h'" src="../img/2h.png">
      <img v-if="i == '3h'" src="../img/3h.png">
      <img v-if="i == '4h'" src="../img/4h.png">
      <img v-if="i == '5h'" src="../img/5h.png">      
      <img v-if="i == '6h'" src="../img/6h.png">
      <img v-if="i == '7h'" src="../img/7h.png">      
      <img v-if="i == '8h'" src="../img/8h.png">
      <img v-if="i == '9h'" src="../img/9h.png">      
      <img v-if="i == '10h'" src="../img/10h.png">
      <img v-if="i == '11h'" src="../img/11h.png">      
      <img v-if="i == '12h'" src="../img/12h.png">
      <img v-if="i == '13h'" src="../img/13h.png">
      <img v-if="i == '14h'" src="../img/1h.png">

      <img v-if="i == '2t'" src="../img/2t.png">
      <img v-if="i == '3t'" src="../img/3t.png">
      <img v-if="i == '4t'" src="../img/4t.png">
      <img v-if="i == '5t'" src="../img/5t.png">      
      <img v-if="i == '6t'" src="../img/6t.png">
      <img v-if="i == '7t'" src="../img/7t.png">      
      <img v-if="i == '8t'" src="../img/8t.png">
      <img v-if="i == '9t'" src="../img/9t.png">      
      <img v-if="i == '10t'" src="../img/10t.png">
      <img v-if="i == '11t'" src="../img/11t.png">      
      <img v-if="i == '12t'" src="../img/12t.png">
      <img v-if="i == '13t'" src="../img/13t.png">
      <img v-if="i == '14t'" src="../img/1t.png">

      <img v-if="i == '2c'" src="../img/2c.png">
      <img v-if="i == '3c'" src="../img/3c.png">
      <img v-if="i == '4c'" src="../img/4c.png">
      <img v-if="i == '5c'" src="../img/5c.png">      
      <img v-if="i == '6c'" src="../img/6c.png">
      <img v-if="i == '7c'" src="../img/7c.png">      
      <img v-if="i == '8c'" src="../img/8c.png">
      <img v-if="i == '9c'" src="../img/9c.png">      
      <img v-if="i == '10c'" src="../img/10c.png">
      <img v-if="i == '11c'" src="../img/11c.png">      
      <img v-if="i == '12c'" src="../img/12c.png">
      <img v-if="i == '13c'" src="../img/13c.png">
      <img v-if="i == '14c'" src="../img/1c.png">

    </div>

<div>
        <img src="../img/back.png" id="deck"/>
</div>

       <!-- <img v-bind:src="'../../../../wwwroot/images/' + playercards[0] + '.png'" id="firstcard"/> -->
       <!-- <img v-bind:src="'../../../../wwwroot/images/' + playercards[1] + '.png'" id="firstcard"/> -->

   <!-- <a>{{playercards[0]}}</a>
   <a>{{playercards[1]}}</a>
   <a>{{playercards[2]}}</a>
   <a>{{playercards[3]}}</a> -->

    <a> Your hand value : {{handvalue}} </a> <br>
    <a> Dealer hand value : {{dealerhandvalue}} </a> <br>
    <a> isiaturn : {{iaturn}} </a> <br>
    <a> RESULT : {{winnerlooser}} </a> <br>

   <form @submit="hit($event)">
   <div style="text-align:center;"><button type="submit" value="hit" class="btn btn-outline-secondary btn-lg" v-if="handvalue < 21 && iaturn == false ">HIT</button></div>
   </form>

   <form @submit="stand($event)">
   <div style="text-align:center;"><button type="submit" value="stand" class="btn btn-outline-secondary btn-lg" v-if="handvalue < 21 && iaturn == false">STAND</button></div>
   </form>

   <form @submit="split($event)">
   <div style="text-align:center;"><button type="submit" value="stand" class="btn btn-outline-secondary btn-lg" v-if="handvalue < 21 && iaturn == false && cansplitplayer == true">STAND</button></div>
   </form>

   <form @submit="playdealer($event)">
   <div style="text-align:center;"><button type="submit" value="playdealer" class="btn btn-outline-secondary btn-lg" v-if="dealerhandvalue < 21 && iaturn == true && gameend == false">PLAY AI</button></div>
   </form>
   
    <router-link to="/play">
      <br><button class="btn btn-dark" v-if="gameend == true">QUITTER</button>
    </router-link>
   </div>
</template>

<script>
import { mapActions } from 'vuex';
import BlackJackApiService from '../services/BlackJackApiService';
import Vue from 'vue';
import GameApiService from '../services/GameApiService';


export default {
    data() {
        return {
            playercards: [],
            dealercards: [],
            handvalue: 0,
            dealerhandvalue: 0,
            iaturn : false,
            gameend: false,
            winnerlooser: '',
            playerwin: false,
            cansplitplayer: false,
            //cansplitia: false,
            nbturn: 0,
        }
    },

  async mounted() {
    this.nbturn = await this.executeAsyncRequest(() => BlackJackApiService.GetTurn());
    await this.refreshCards();
    await this.refreshHandValue();
    this.CheckWinner();
  },

    methods: {
        ...mapActions(['executeAsyncRequest']),


    async hit(e) {
        await this.executeAsyncRequest(() => BlackJackApiService.HitPlayer());
        if(this.handvalue > 21) {
            this.gameend = true;
        }
    },

     stand(e) {
        e.preventDefault();
        this.iaturn =  BlackJackApiService.StandPlayer();
        this.iaturn = true;
        this.refreshCards();
        this.refreshHandValue();
    },

    refreshiaturn() {
        this.iaturn = BlackJackApiService.refreshAiturn();
    },

    CanSplitPlayer() {
        this.cansplitplayer = BlackJackApiService.CanSplitPlayer();
    },

    // CanSplitIa() {
    //     this.cansplitia = BlackJackApiService.CanSplitAi();
    // },

    async playdealer(e) {
        e.preventDefault();
        await this.executeAsyncRequest(() => BlackJackApiService.PlayAI());
        await this.refreshCards();
        await this.refreshHandValue();
        this.CheckWinner();
        this.gameend = true;
    },

     CheckWinner() {
        if(this.gameend == true || this.handvalue == this.dealerhandvalue && this.iaturn == true || this.handvalue == 21 || this.handvalue > 21 || this.dealerhandvalue == 21 || this.dealerhandvalue > 21) {
        if(this.handvalue > 21 ) {
            this.winnerlooser = 'You loose';
        } else if(this.dealerhandvalue > 21) {
            this.winnerlooser = 'You win'
            this.playerwin = true;
        } else if(this.dealerhandvalue == 21) {
            this.winnerlooser = 'BLACKJACK! You loose'
        } else if(this.handvalue == 21) {
            this.winnerlooser = 'BLACKJACK ! You win';
            this.playerwin = true;
        } else if(this.handvalue < this.dealerhandvalue ) {
            this.winnerlooser = 'You loose';
        }  else if(this.handvalue > this.dealerhandvalue) {
            this.winnerlooser = 'You win';
            this.playerwin = true;
        } else if(this.handvalue == this.dealerhandvalue) {
            this.winnerlooser = "Draw";
        }
         this.gameend = true;
         this.updateStats();
        }
    },

    async updateStats() {
        await this.executeAsyncRequest(() => GameApiService.UpdateStats('BlackJack',this.playerwin));
    },

    async refreshHandValue() {
        this.handvalue = await this.executeAsyncRequest(() => BlackJackApiService.getplayerHandValue());
        this.dealerhandvalue = await this.executeAsyncRequest(() => BlackJackApiService.getAiHandValue());
    },

    async refreshCards() {
      this.playercards = await this.executeAsyncRequest(() => BlackJackApiService.GetPlayerCards());
      this.dealercards = await this.executeAsyncRequest(() => BlackJackApiService.GetAiCards());
    },


    }
}
</script>

<style>
 #deck {
     height: 215px;
     width: 135px;
 }

 .playercards > img {
     height: 205px;
     width: 125px;
 }

 .playercards {
	display: inline-block;
	position: relative;
	
 }

 .dealercards {
	display: inline-block;
	position: relative;
	left: 2.2%
 }

 .dealercards > img {
     height: 205px;
     width: 125px;
     left: 175%;
 }

</style>



