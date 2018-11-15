<template>
  <div id="home">
    <div class="games">
      <div class="yams">
        <router-link v-on:click.native="PlayYams(0)" to="">
          <img src="../img/LOGO1.png" alt="yams" class="imgyams">
        </router-link>
        <img src="../img/LOGO2.png" alt="textyams" class="textyams">
      </div>
      
      <div class="blackjack">
      <router-link v-on:click.native="PlayBlackJack(1)" to="">
        <img src="../img/LOGO3.png" alt="blackjack" class="imgblackjack">
      </router-link>
      <img src="../img/LOGO4.png" alt="textbj" class="textbj">
      </div>
    </div>

  </div>
</template>

<script>
import { mapActions } from 'vuex';
import GameApiService from '../services/GameApiService';
import YamsApiService from '../services/YamsApiService';
import UserApiService from '../services/UserApiService';
import BlackJackApiService from '../services/BlackJackApiService';

import Vue from 'vue';

export default {
  data() {
    return {
      isInGameYams: false,
      isInGameBlackJack: false
    }
  },

  async mounted() {
    
    await this.GetisInGame();
    await this.DeleteAis();
    
  },

  methods: {
    ...mapActions(['executeAsyncRequest']),

      async GetisInGame() {
        this.isInGameYams = await this.executeAsyncRequest(() => GameApiService.isInGame(0))
        this.isInGameBlackJack = await this.executeAsyncRequest(() => GameApiService.isInGame(1));
      },

      async DeleteAis() {

      if(this.isInGameBlackJack != true) {
         await this.executeAsyncRequest(() => BlackJackApiService.DeleteJackAiPlayer());
         console.log("condition 1");
      }
      
      if(this.isInGameYams != true) {
         await this.executeAsyncRequest(() => YamsApiService.DeleteYamsAiPlayer());
         console.log("condition 2");
      }
      
      if(this.isInGameBlackJack != true && this.isInGameYams != true) {
         await this.executeAsyncRequest(() => GameApiService.DeleteAis());
         console.log("condition 3");
      }   
    },

    async PlayYams(gameTypeId) {

      if(this.isInGameYams != true) {
          await this.executeAsyncRequest(() => GameApiService.createGame(gameTypeId));      
          await this.executeAsyncRequest(() => GameApiService.createAiUser());
          await this.executeAsyncRequest(() => YamsApiService.CreateYamsPlayer());
          await this.executeAsyncRequest(() => YamsApiService.CreateYamsAiPlayer());
      }


      // } catch (error) {
      //    console.log(error);
      //    alert("Vous devez finir vos parties en cours avant de relancer une autre partie");
      //    return;
      // }

      //IF IS NOT INGAME YAMS


      //ELSE
      //REPRENDRE LA GAME

      this.$router.push({ path: 'yams' });
    },

    async PlayBlackJack(gameTypeId) {
      
      if(this.isInGameBlackJack != true) {
       await this.executeAsyncRequest(() => GameApiService.createGame(gameTypeId));
       await this.executeAsyncRequest(() => GameApiService.createAiUser());

       await this.executeAsyncRequest(() => BlackJackApiService.CreateJackPlayer());
       await this.executeAsyncRequest(() => BlackJackApiService.CreateJackAiPlayer());
       await this.executeAsyncRequest(() => BlackJackApiService.InitPlayer());
       await this.executeAsyncRequest(() => BlackJackApiService.InitIa());
      }

      
        // REPRENDRE LA GAME
      
      this.$router.push({ path: 'blackJack' });
    }
  }
}
</script>

<style>


/* DISPLAY POSITION */
.games {
  padding-top: 15%;
  padding-left: 34%;
  display: inline-flex;
  display: center;
}

/* YAMS */
.imgyams {
  transition: transform .4s;
  height: 220px;
  width: 255px;
}

.imgyams:hover{
    transform: scale(1.4);
}

.yams:hover .textyams {
visibility: visible;
}

.textyams {
  height: 35px;
  width: 100px;
  transition: transform .4s;
  visibility: hidden;
}

/* BLACK JACK*/
.imgblackjack {
  transition: transform .4s;   
  height: 220px;
  width: 255px;
}

.imgblackjack:hover {
    transform: scale(1.4); 
}

.blackjack:hover .textbj {
visibility: visible;
}

.textbj {
  height: 170px;
  width: 210px;
  transition: transform .4s;
  visibility: hidden;
}


</style>


  