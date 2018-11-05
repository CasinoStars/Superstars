<template>
  <div id="home">
    <div class="games">
      <div class="yams">
        <router-link v-on:click.native="PlayYams('Yams')" to="">
          <img src="../img/LOGO1.png" alt="yams" class="imgyams">
        </router-link>
        <img src="../img/LOGO2.png" alt="textyams" class="textyams">
      </div>
      
      <div class="blackjack">
      <router-link v-on:click.native="PlayBlackJack('BlackJack')" to="">
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


  async mounted() {
    await this.DeleteAis();
  },

  methods: {
    ...mapActions(['executeAsyncRequest']),

    async DeleteAis() {

      if(await this.executeAsyncRequest(() => BlackJackApiService.Getisingame()) == 0 ) {
         await this.executeAsyncRequest(() => BlackJackApiService.DeleteJackAiPlayer());
      }

      if (await this.executeAsyncRequest(() => YamsApiService.Getisingame()) == 0) {
         await this.executeAsyncRequest(() => YamsApiService.DeleteYamsAiPlayer());
      }

      if (await this.executeAsyncRequest(() => YamsApiService.Getisingame()) == 0 && await this.executeAsyncRequest(() => BlackJackApiService.Getisingame()) == 0) {
         await this.executeAsyncRequest(() => GameApiService.DeleteAis());
      }
    },

    async PlayYams(gametype) {
      if (await this.executeAsyncRequest(() => YamsApiService.Getisingame()) == 0) {
          await this.executeAsyncRequest(() => GameApiService.createGame(gametype));
      try {
         await this.executeAsyncRequest(() => GameApiService.createAiUser());
      } catch (error) {
         console.log(error);
         alert("Vous devez finir vos parties en cours avant de relancer une autre partie");
         return;
      }
      await this.executeAsyncRequest(() => YamsApiService.CreateYamsPlayer());
      await this.executeAsyncRequest(() => YamsApiService.CreateYamsAiPlayer());
      } else {
        //REPRENDRE LA GAME
      }
      this.$router.push({ path: 'yams' });
    },

    async PlayBlackJack(gametype) {
      if(await this.executeAsyncRequest(() => BlackJackApiService.Getisingame()) == 0 ) {
       await this.executeAsyncRequest(() => GameApiService.createGame(gametype));
       try {
         await this.executeAsyncRequest(() => GameApiService.createAiUser());
       } catch(error) {
         alert("Vous devez finir vos parties en cours avant de relancer une autre partie");
         return;
       }
       await this.executeAsyncRequest(() => BlackJackApiService.CreateJackPlayer());
       await this.executeAsyncRequest(() => BlackJackApiService.CreateJackAiPlayer());
       await this.executeAsyncRequest(() => BlackJackApiService.InitPlayer());
       await this.executeAsyncRequest(() => BlackJackApiService.InitIa());
      } else {
        // REPRENDRE LA GAME
      }
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


  