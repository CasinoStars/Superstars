<template>
<div class="page">

  <div class="iadices">
    <img src="../img/diceia1.png" alt="iadice1" id="iadice1">
    <img src="../img/diceia2.png" alt="iadice2" id="iadice2">
    <img src="../img/diceia3.png" alt="iadice3" id="iadice3">
    <img src="../img/diceia4.png" alt="iadice4" id="iadice4">
    <img src="../img/diceia5.png" alt="iadice5" id="iadice5">
  </div>

  <form @submit="onSubmit($event)">
    <div class="playerdices">
      <input type="checkbox" id="dice1" value="1" v-model="selected">        
      <img src="../img/dice1.png" alt="dice1" id="playerdice1">
      <input type="checkbox" id="dice2" value="2" v-model="selected">        
      <img src="../img/dice2.png" alt="dice2" id="playerdice2">
      <input type="checkbox" id="dice3" value="3" v-model="selected">        
      <img src="../img/dice3.png" alt="dice3" id="playerdice3">
      <input type="checkbox" id="dice4" value="4" v-model="selected">        
      <img src="../img/dice4.png" alt="dice4" id="playerdice4">
      <input type="checkbox" id="dice5" value="5" v-model="selected">
      <img src="../img/dice5.png" alt="dice5" id="playerdice5">
    </div>

  <br><div style="text-align:center;">relanceDice: <strong>{{ selected }}</strong></div>

  <strong> {{ dices }} </strong>
  <li v-for="i of dices" :key="i">
    <img v-bind:src='`/../img/dice${i}.png`' :alt='`dice${i}`'>
  </li>

  <br><div style="text-align:center;"><button type="submit" class="btn btn-outline-secondary btn-lg">ROLL</button></div>
  </form>
</div>
</template>

<script>
import { mapActions } from 'vuex';
import YamsApiService from '../services/YamsApiService';
import UserApiService from '../services/UserApiService';
import Vue from 'vue';

export default {
  data(){
    return {
      selected: [],
      dices: []
    }
  },

  async mounted() {
    await this.refreshDices();
  },
  
  methods: {
    ...mapActions(['executeAsyncRequest']),

    async refreshDices() {
      this.dices = await this.executeAsyncRequest(() => YamsApiService.GetPlayerDices(UserApiService.pseudo));
    },

    async onSubmit(e) {
      await this.executeAsyncRequest(() => YamsApiService.RollDices(UserApiService.pseudo, dices, selected));
    }
  }
}
</script>

<style>
  .iadices {
    display: block;
    margin-left: auto;
    margin-right: auto;
    margin-top: 8%;
    width: 36%;
}

  .playerdices {
    display: block;
    margin-left: auto;
    margin-right: auto;
    margin-top: 10%;
    width: 43%;
}

.iadices > img {
  height: 100px;
  width: 100px;  
}

.playerdices > img {
  height: 100px;
  width: 100px;  
}
</style>


  