<template>
    <div>
      <br><br>
        <div style="text-align: center;margin-top 2%;font-family: 'Courier New', sans-serif;">
          <h1 style="font-variant: small-caps; font-size: 45px;"> <strong>Classement</strong></h1>
        </div>
  <br><br>
  <button v-on:click="SwapTrueOrFake()" class="btn btn dark" style="font-family: 'Courier New', sans-serif;">Vrai Argent / Faux Argent</button>

<table v-if=TrueOrFake>
  <tr>
    <th>Pseudo</th>
    <th>Profit Bitcoin</th>
    <th>Parties de Yams joué</th>
    <th>Parties de BlackJack joué</th>
  </tr>
  <tr>
    <th>
    <div v-for="(e,index) of pseudos" :key='index'>
       <td>{{e}}</td>
    </div>
    </th>
    <th>
    <div v-for="(e,index) of profits" :key='index'>
       <td>{{e}}</td>
    </div>
    </th>
     <th>
    <div v-for="(e,index) of playeryamsnbgames" :key='index'>
       <td>{{e}}</td>
    </div>
    </th>
     <th>
    <div v-for="(e,index) of playerblackjacknbgames" :key='index'>
       <td>{{e}}</td>
    </div>
    </th>
  </tr>
</table>
<table v-else>
  <tr>
    <th>Pseudo</th>
    <th>Profit Fausse Monnaie</th>
    <th>Parties de Yams joué</th>
    <th>Parties de BlackJack joué</th>
  </tr>
  <tr>
    <th>
    <div v-for="(e,index) of pseudos" :key='index'>
       <td>{{e}}</td>
    </div>
    </th>
    <th>
    <div v-for="(e,index) of profits" :key='index'>
       <td>{{e}}</td>
    </div>
    </th>
     <th>
    <div v-for="(e,index) of playeryamsnbgames" :key='index'>
       <td>{{e}}</td>
    </div>
    </th>
     <th>
    <div v-for="(e,index) of playerblackjacknbgames" :key='index'>
       <td>{{e}}</td>
    </div>
    </th>
  </tr>
</table>
 <br>
 <br>
 <br>
 <br>
 <br>

</div>
</template>

<script>

import { mapActions } from 'vuex';
import Vue from 'vue';
import GameApiService from '../services/GameApiService';
import WalletApiService from '../services/WalletApiService';
import UserApiService from '../services/UserApiService';
import RankApiService from '../services/RankApiService';


export default {
    data() {
        return {
            TrueOrFake: true,
            pseudos: [],
            profits: [],
            playerblackjacknbgames: [],
            playeryamsnbgames: []
        }
    },    

    async mounted() {
      this.pseudos = await this.executeAsyncRequest(() => RankApiService.GetPlayersUserNameSorted(this.TrueOrFake));
      this.profits = await this.executeAsyncRequest(() => RankApiService.GetPlayersProfitSorted(this.TrueOrFake));
      this.playeryamsnbgames = await this.executeAsyncRequest(() => RankApiService.GetPlayersYamsNumberParts(this.TrueOrFake));
      this.playerblackjacknbgames = await this.executeAsyncRequest(() => RankApiService.GetPlayersBlackJackNumberParts(this.TrueOrFake));
    },

     methods: {
    ...mapActions(['executeAsyncRequest']),
      SwapTrueOrFake(){
        this.TrueOrFake = !this.TrueOrFake;
        this.changethetab();
        console.log(this.TrueOrFake);
      },

      async changethetab(){
      this.pseudos = await this.executeAsyncRequest(() => RankApiService.GetPlayersUserNameSorted(this.TrueOrFake));
      this.profits = await this.executeAsyncRequest(() => RankApiService.GetPlayersProfitSorted(this.TrueOrFake));
      this.playeryamsnbgames = await this.executeAsyncRequest(() => RankApiService.GetPlayersYamsNumberParts(this.TrueOrFake));
      this.playerblackjacknbgames = await this.executeAsyncRequest(() => RankApiService.GetPlayersBlackJackNumberParts(this.TrueOrFake));
      }
    
     }
}
</script>

 <style>
 
 </style>
 
