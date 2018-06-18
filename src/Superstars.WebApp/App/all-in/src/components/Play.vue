<template>
  <div id="home">
    <div class="games">
      <div class="yams">
        <router-link v-on:click.native="PlayYams('Yams')" to="">
          <img src="../img/LOGO1.png" alt="yams" id="imgyams">
        </router-link>
        <img src="../img/LOGO2.png" alt="textyams" id="textyams">
      </div>

      <div class="blackjack">
      <router-link v-on:click.native="PlayBlackJack('BlackJack')" to="">
        <img src="../img/LOGO3.png" alt="blackjack" id="imgblackjack">
      </router-link>
      <img src="../img/LOGO4.png" alt="textbj" id="textbj">
      </div>

      <img src="../img/LOGO1.png" alt="poker" id="imgpoker">           

    </div>
  </div>
</template>

<script>
import { mapActions } from 'vuex';
import GameApiService from '../services/GameApiService';
import YamsApiService from '../services/YamsApiService';
import BlackJackApiService from '../services/BlackJackApiService';

import Vue from 'vue';

export default {

  async mounted() {
    await this.DeleteAis();
  },

  methods: {
    ...mapActions(['executeAsyncRequest']),

    async DeleteAis() {
      await this.executeAsyncRequest(() => BlackJackApiService.DeleteJackAiPlayer());
      await this.executeAsyncRequest(() => YamsApiService.DeleteYamsAiPlayer());
      await this.executeAsyncRequest(() => GameApiService.DeleteAis());
    },

    async PlayYams(gametype) {
      await this.executeAsyncRequest(() => GameApiService.createGame(gametype));
      await this.executeAsyncRequest(() => GameApiService.createAiUser());
      await this.executeAsyncRequest(() => YamsApiService.CreateYamsPlayer());
      await this.executeAsyncRequest(() => YamsApiService.CreateYamsAiPlayer());
      this.$router.push({ path: 'yams' });
    },

    async PlayBlackJack(gametype) {
      await this.executeAsyncRequest(() => GameApiService.createGame(gametype));
      await this.executeAsyncRequest(() => GameApiService.createAiUser());
      await this.executeAsyncRequest(() => BlackJackApiService.CreateJackPlayer());
      await this.executeAsyncRequest(() => BlackJackApiService.CreateJackAiPlayer());
      await this.executeAsyncRequest(() => BlackJackApiService.InitPlayer());
      await this.executeAsyncRequest(() => BlackJackApiService.InitIa());
      this.$router.push({ path: 'blackJack' });
    }
  }
}
</script>

<style>

  .games {
  margin: 290px;
  margin-left: 400px;
  display: -webkit-inline-box;
}

.textgames {
  margin-top: 190px;
  margin-left: 55px;
}

#textyams {
  height: 35px;
  width: 100px;
  transition: transform .4s;
  visibility: hidden;
}


#imgyams {
  transition: transform .4s;
  height: 180px;
  width: 215px;
}

#imgyams:hover{
    transform: scale(1.4);
}

.yams:hover #textyams {
visibility: visible;
}

#imgpoker {
  transition: transform .4s;   
  height: 180px;
  width: 215px;
}

#imgpoker:hover {
    transform: scale(1.4); 
}

#imgblackjack {
  transition: transform .4s;   
  height: 180px;
  width: 215px;
}

#imgblackjack:hover {
    transform: scale(1.4); 
}

.blackjack:hover #textbj {
visibility: visible;
}

#textbj {
  height: 170px;
  width: 210px;
  transition: transform .4s;
  visibility: hidden;
}


</style>


  