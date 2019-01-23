<template>
<div class="container-fluid">
  <div class="row play">
    <div class="col-md-4 yams">
      <router-link v-on:click.native="PlayYams(0)" to="" class="routeryams">
        <img src="../img/LOGO1.png" alt="yams" class="imgyams">
      </router-link>
      <p style="font-family: Bebas; font-size: 5.2vh; text-align: center;"> Yam's </p>
    </div>

    <div class="col-md-4 blackjack">
      <router-link v-on:click.native="PlayBlackJack(1)" to="" class="routerbj">
        <img src="../img/LOGO3.png" alt="blackjack" class="imgblackjack">
      </router-link>
      <p style="font-family: Bebas; font-size: 5.2vh; text-align: center;"> BlackJack </p>
    </div>

    <div class="col-md-4 crash">
      <router-link to="/crash" class="routercrash">
        <img src="../img/LOGO5.png" alt="crash" class="imgcrash">
      </router-link>
      <p style="font-family: Bebas; font-size: 5.2vh; text-align: center;"> Crash </p>
    </div>
  </div>
  </div>
</template>

<script>
  import {
    mapActions
  } from 'vuex';
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

        if (this.isInGameBlackJack != true) {
          await this.executeAsyncRequest(() => BlackJackApiService.DeleteJackAiPlayer());
        }

        if (this.isInGameYams != true) {
          await this.executeAsyncRequest(() => YamsApiService.DeleteYamsAiPlayer());
        }
      },

      async PlayYams(gameTypeId) {

        if (this.isInGameYams != true) {
          await this.executeAsyncRequest(() => GameApiService.createGame(gameTypeId));
          await this.executeAsyncRequest(() => YamsApiService.CreateYamsPlayer());
          await this.executeAsyncRequest(() => YamsApiService.CreateYamsAiPlayer());
        }
        this.$router.push({
          path: 'yams'
        });
      },

      async PlayBlackJack(gameTypeId) {

        if (this.isInGameBlackJack != true) {
          await this.executeAsyncRequest(() => GameApiService.createGame(gameTypeId));
          await this.executeAsyncRequest(() => BlackJackApiService.CreateJackPlayer());
          await this.executeAsyncRequest(() => BlackJackApiService.CreateJackAiPlayer());
          await this.executeAsyncRequest(() => BlackJackApiService.InitPlayer());
          await this.executeAsyncRequest(() => BlackJackApiService.InitIa());
        }
        this.$router.push({
          path: 'blackJack'
        });
      }
    }
  }
</script>

<style lang="scss">

 @font-face { 
   font-family: Bebas; src: url('../fonts/BEBAS.ttf');
   } 

  @-webkit-keyframes rotateYams {
    0% {
      -webkit-transform: rotate(0deg);
    }

    50% {
      -webkit-transform: rotate(180deg);
    }

    100% {
      -webkit-transform: rotate(360deg);
    }
  }

  @-webkit-keyframes scaleBJ {
    0% {
      -webkit-transform: scale(1.0);
    }

    50% {
      -webkit-transform: scale(1.4);
    }

    100% {
      -webkit-transform: scale(1.0);
    }
  }

  @-webkit-keyframes shakingCrash {
    0% {
      -webkit-transform: translate(2px, 1px) rotate(0deg);
    }

    10% {
      -webkit-transform: translate(-1px, -2px) rotate(-1deg);
    }

    20% {
      -webkit-transform: translate(-3px, 0px) rotate(1deg);
    }

    30% {
      -webkit-transform: translate(0px, 2px) rotate(0deg);
    }

    40% {
      -webkit-transform: translate(1px, -1px) rotate(1deg);
    }

    50% {
      -webkit-transform: translate(-1px, 2px) rotate(-1deg);
    }

    60% {
      -webkit-transform: translate(-3px, 1px) rotate(0deg);
    }

    70% {
      -webkit-transfor: translate(2px, 1px) rotate(-1deg);
    }

    80% {
      -webkit-transform: translate(-1px, -1px) rotate(1deg);
    }

    90% {
      -webkit-transform: translate(2px, 2px) rotate(0deg);
    }

    100% {
      -webkit-transform: translate(1px, -2px) rotate(-1deg);
    }
  }

  @media(max-width: 562px) {
    .play {
      text-align: center;
      display: block;
    }

    .play .textbj {
      height: 30%;
      width: 50%;
      transition: transform .4s;
    }

    .play .textcrash {
      height: 30%;
      width: 40%;
      transition: transform .4s;
    }
  }

  @media(min-width: 562px) {
    .play {
      margin-top: 10%;
      margin-left: 10%;
      display: inline-flex;
    }

    .play .yams .textyams {
      height: 30%;
      width: 40%;
      transition: transform .4s;
      visibility: visible;
      // margin-left: 12%;
    }

    .play .textbj {
      height: 30%;
      width: 50%;
      transition: transform .4s;
      //margin-left: 7%;
    }

    .play .textcrash {
      height: 30%;
      width: 40%;
      transition: transform .4s;
      //margin-left: 12%;
    }
  }


  .play {
    text-align: center;
  }
  
  .play > div {
    display:inline-block;
    margin:auto; 
  }

  /* YAMS */
  .play .yams .imgyams {
    transition: transform .4s;
    height: 70%;
    width: 70%;
    margin-left: auto;
    margin-right: auto;
  }

  .yams > p {
    margin-left: -1%;
  }
  
  .play .yams .routeryams:hover {
    .imgyams {
      -webkit-animation-name: rotateYams;
      -webkit-animation-duration: 3s;
      -webkit-animation-iteration-count: infinite;
      -webkit-animation-timing-function: linear;
    }

  }

  /* BLACK JACK*/
  .play .imgblackjack {
    transition: transform .4s;
    height: 70%;
    width: 70%;
  }

  .blackjack > p {
    margin-left: -1%;
  }

  .play .routerbj:hover {
    .imgblackjack {
      -webkit-animation-name: scaleBJ;
      -webkit-animation-duration: 3s;
      -webkit-animation-iteration-count: infinite;
      -webkit-animation-timing-function: linear;
    }

  }

  /* CRASH */
  .play .imgcrash {
    transition: transform .4s;
    height: 70%;
    width: 70%;
  }

  .crash > p {
    margin-left: -1%;
  }

  .play .crash .routercrash:hover {
    .imgcrash {
      -webkit-animation-name: shakingCrash;
      -webkit-animation-duration: 0.8s;
      -webkit-transform-origin: 50% 50%;
      -webkit-animation-iteration-count: infinite;
      -webkit-animation-timing-function: linear;
    }

  }
</style>