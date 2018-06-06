<template>
<div class="page">
<div style="letter-spacing: 2px; font-family: 'Courier New', sans-serif; margin-top:2%; margin-left:90%;"> <h3> TOUR:{{nbTurn}} </h3></div>
<form @submit="onSubmitAI($event)">
    <div v-for="(i, index) of iadices" :key="index" class="playerdices">
      <img v-if="i == 1" src="../img/diceia1.png">
      <img v-if="i == 2" src="../img/diceia2.png">
      <img v-if="i == 3" src="../img/diceia3.png">
      <img v-if="i == 4" src="../img/diceia4.png">
      <img v-if="i == 5" src="../img/diceia5.png">
    </div><br>
    <br><div style="text-align:center;"><button type="submit" class="btn btn-outline-secondary btn-lg" v-if="nbTurn >= 3">ROLL IA</button></div>
</form>
  <br><br><br>
  <form @submit="onSubmit($event)">
    <div v-for="(i, index) of dices" :key="index" class="playerdices">
      <img v-if="i == 1" src="../img/dice1.png">
      <img v-if="i == 2" src="../img/dice2.png">
      <img v-if="i == 3" src="../img/dice3.png">
      <img v-if="i == 4" src="../img/dice4.png">
      <img v-if="i == 5" src="../img/dice5.png">
    </div><br>
    <div class="checkbox" v-if="nbTurn != 0 && nbTurn < 3">
      <input type="checkbox" id="dice1" value="1" v-model="selected">        
      <input type="checkbox" id="dice2" value="2" v-model="selected">
      <input type="checkbox" id="dice3" value="3" v-model="selected">        
      <input type="checkbox" id="dice4" value="4" v-model="selected">        
      <input type="checkbox" id="dice5" value="5" v-model="selected">
    </div>
  <br><div  style="text-align:center;">relanceDice: <strong>{{ selected }}</strong></div>


  <br><div style="text-align:center;"><button type="submit" class="btn btn-outline-secondary btn-lg" v-if="nbTurn < 3">ROLL</button></div>

  </form>
</div>
</template>

<script>
import { mapActions } from 'vuex';
import YamsApiService from '../services/YamsApiService';
import Vue from 'vue';

export default {
  data(){
    return {
      selected: [],
      dices: [],
      iadices: [],
      nbTurn: 0,
    }
  },

  async mounted() {
    await this.refreshDices();
  },
  
  methods: {
    ...mapActions(['executeAsyncRequest']),

    async refreshDices() {
      this.dices = await this.executeAsyncRequest(() => YamsApiService.GetPlayerDices());
      this.iadices = await this.executeAsyncRequest(() => YamsApiService.GetIaDices());
    },

    async changeTurn() {
      this.nbTurn = await this.executeAsyncRequest(() => YamsApiService.GetTurn());
    },

    async onSubmitAI(e) {
      e.preventDefault();
      if(this.nbTurn == 3)
      this.iadices = await this.executeAsyncRequest(() => YamsApiService.GetIaDices());
      let arraydice = [this.iadices, this.dices];
      console.log(arraydice);
      await this.executeAsyncRequest(() => YamsApiService.RollIaDices(arraydice));
      await this.refreshDices();
    },

    async onSubmit(e) {
      e.preventDefault();

      if(this.nbTurn === 0)
        await this.executeAsyncRequest(() => YamsApiService.RollDices([1,2,3,4,5]));
      else
        await this.executeAsyncRequest(() => YamsApiService.RollDices(this.selected));
      await this.refreshDices();

      if(this.nbTurn < 3)
      await this.changeTurn();
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
	display: inline-block;
	position: relative;
  width: 8%;
	left: 30.2%
}

  .checkbox {
    text-align: center;
    letter-spacing: 100px;
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


  