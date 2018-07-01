<template>
    <div>
        <div style="text-align: center;margin-top 2%;font-family: 'Courier New', sans-serif;">
      <h1 style="font-variant: small-caps;"> <strong> Mes statistiques  </strong></h1>
  </div>
  <br><br>
<table>
  <tr>
    <th>Pseudo</th>
    <th>TrueProfit</th>
    <th>FakeProfit</th>
    <th>Nombre de parties</th>
  </tr>
  <tr>
    <td>{{Pseudo}}</td>
    <td>{{playertrueprofit}}</td>
    <td>{{playerfakeprofit}}</td>
    <td>{{playernbgames}}</td>
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


export default {
    data() {
        return {
            pseudos: {},
            playertrueprofit: 0,
            playerfakeprofit: 0,
            playernbgames: 0
        }
    },

    async mounted() {

    },

     methods: {
    ...mapActions(['executeAsyncRequest']),


     }
}
</script>

 <style>
 
 </style>
 
