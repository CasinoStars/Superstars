<template>
  <div class="play">
    <div class="yams">
      <router-link v-on:click.native="PlayYams(0)" to="" class="routeryams">
        <img src="../img/LOGO1.png" alt="yams" class="imgyams">
        <br>
        <img src="../img/YamsText.png" alt="textyams" class="textyams">
      </router-link>
    </div>

    <div class="blackjack">
      <router-link v-on:click.native="PlayBlackJack(1)" to="" class="routerbj">
        <img src="../img/LOGO3.png" alt="blackjack" class="imgblackjack">
        <br>
        <img src="../img/BlackJackText.png" alt="textbj" class="textbj">
      </router-link>
    </div>

    <div class="crash">
      <router-link to="/crash" class="routercrash">
        <img src="../img/LOGO5.png" alt="crash" class="imgcrash">
        <br>
        <img src="../img/CrashText.png" alt="textcrash" class="textcrash">
      </router-link>
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

    .play .yams .textyams {
      height: 30%;
      width: 40%;
      transition: transform .4s;
      visibility: visible;
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
      margin-top: 15%;
      margin-left: 10%;
      display: inline-flex;
    }

    .play .yams .textyams {
      height: 30%;
      width: 40%;
      transition: transform .4s;
      visibility: visible;
      margin-left: 12%;
    }

    .play .textbj {
      height: 30%;
      width: 50%;
      transition: transform .4s;
      margin-left: 7%;
    }

    .play .textcrash {
      height: 30%;
      width: 40%;
      transition: transform .4s;
      margin-left: 12%;
    }
  }

  /* YAMS */
  .play .yams .imgyams {
    transition: transform .4s;
    height: 70%;
    width: 70%;
  }

  .play .yams .routeryams:hover {
    .imgyams {
      -webkit-animation-name: rotateYams;
      -webkit-animation-duration: 3s;
      -webkit-animation-iteration-count: infinite;
      -webkit-animation-timing-function: linear;
    }

    .textyams {
      opacity: 0.7;
    }
  }

  /* BLACK JACK*/
  .play .imgblackjack {
    transition: transform .4s;
    height: 70%;
    width: 70%;
  }

  .play .routerbj:hover {
    .imgblackjack {
      -webkit-animation-name: scaleBJ;
      -webkit-animation-duration: 3s;
      -webkit-animation-iteration-count: infinite;
      -webkit-animation-timing-function: linear;
    }

    .textbj {
      opacity: 0.7;
    }
  }

  /* CRASH */
  .play .imgcrash {
    transition: transform .4s;
    height: 70%;
    width: 70%;
  }

  .play .crash .routercrash:hover {
    .imgcrash {
      -webkit-animation-name: shakingCrash;
      -webkit-animation-duration: 0.8s;
      -webkit-transform-origin: 50% 50%;
      -webkit-animation-iteration-count: infinite;
      -webkit-animation-timing-function: linear;
    }

    .textcrash {
      opacity: 0.7;
    }
  }
</style>