<template>
<div class="tab">
  <div class="header">LOG HUB</div>
  <el-row>
    <el-col :span="18">
      <div class="logs">
        <div class="log-name">
          <span class="client-type">&nbsp;&nbsp;Client type</span>
          <span class="log-content">&nbsp;&nbsp;Log dynamic</span>
        </div>
        <el-tabs tab-position="left"  @tab-click="getFileList" >
          <el-tab-pane v-if="logList && logList.length" v-for="(log,index) in logList" :key='index' :label="log.name" style="background: white">
            <div v-if="log.content && log.content.length > 0" v-for="content in log.content" style="background: white;margin-left: 15px;margin-top: 10px;overflow: hidden;text-overflow: ellipsis;white-space: nowrap">{{content}}</div>
          </el-tab-pane>
        </el-tabs>
      </div>
    </el-col>
    <el-col :span="6">
      <div class="download">
        <p style="background: white;font-weight: bold">&nbsp;&nbsp;Historical Log</p>
          <ul>
<<<<<<< HEAD
            <li style="background: white"v-for="file in fileList" ><span style="background: white" >&nbsp;{{file}}</span><a  v-if="fileList && fileList.length" :href="'http://localhost:19222/api/Log/GetFile?clientName='+clientName+'&fileName='+file" download="logs" style="background: white"><i class="el-icon-download" style="background: white"></i></a></li>
=======
            <li style="background: white"v-for="file in fileList" ><span style="background: white" >&nbsp;{{file}}</span><a  v-if="fileList && fileList.length" :href="'http://192.168.2.116:19222/api/Log/GetFile?clientName='+clientName+'&fileName='+file" download="logs" style="background: white"><i class="el-icon-download" style="background: white"></i></a></li>
>>>>>>> 9d3b3c587ba7450281160d1c923160963142c1f8
          </ul>
      </div>
    </el-col>
  </el-row>

</div>
</template>

<script>
  import $ from 'jquery'
  let _this;
export default {
  name: 'HelloWorld',
  data() {
    return {
      value: "",
      logList:[],
      fileList:[],
      clientName:''
    }
  },

  mounted(){
    _this = this;
    const connection = new this.signalr.HubConnectionBuilder()
<<<<<<< HEAD
      .withUrl("http://localhost:19222/pushLogHub")
=======
      .withUrl("http://192.168.2.115:19222/pushLogHub")
>>>>>>> 9d3b3c587ba7450281160d1c923160963142c1f8
      .build();
    connection.on('ConnectionSuccess', function (message) {
      console.log("ConnectionSuccess and id:", message);
    })

    connection.on('ReciveMessage', function (message) {
      if(_this.logList && _this.logList.length > 0){
        let index = _this.logList.findIndex(function(item){
         return item.name == message.clientName;
        });
        if(index > -1){
          _this.logList[index].content.unshift(message.content);
        }else{
          _this.logList.unshift({id:message.appId, name:message.clientName, content:[message.content]});
        }
      }else{
        _this.logList.unshift({id:message.appId, name:message.clientName, content:[message.content]});
      }
    });
    connection.serverTimeoutInMilliseconds = 15000; // 100 second

    connection.start();
  },
  methods:{
     getFileList:function (item) {
      /* console.log(item)
       console.log(item.label);*/
       this.clientName=item.label;
<<<<<<< HEAD
       this.$ajax.get('http://localhost:19222/api/Log/GetFilesName?clientName='+item.label)
=======
       this.$ajax.get('http://192.168.2.115:19222/api/Log/GetFilesName?clientName='+item.label)
>>>>>>> 9d3b3c587ba7450281160d1c923160963142c1f8
         .then(function (res) {
           if(res.data){
             _this.fileList = res.data
           }else{
             _this.fileList = [];
           }
         })
       console.log(_this.fileList)
     },
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
@import "../assets/css/index.css";
</style>
<style>
  *{
    margin: 0;
    padding: 0;
    background: #ecedf2;
  }
  .log-name{
    height: 40px;
    font-weight: bold;
    width: 100%;
  }
  .client-type{
    margin-left: 40px!important;
    width: 258px;
    background: white;
    height: 40px;
    line-height: 40px;
    display: inline-block;
  }
  .log-content{
    width: 68%;
    background: white;
    height: 40px;
    display: inline-block;
    line-height: 40px;
    margin-left: 4px;
  }
 .tab{
  /*background: #ecedf2;*/
 }
  .tab .el-tabs--left .el-tabs__item.is-left {
    text-align: left;
    background: white;
  }
  .tab .el-tabs--left .el-tabs__nav.is-left{
    margin-left: 40px!important;
    width: 258px;
  }
  /*.tab .el-tabs--left .el-tabs__item.is-left{
    text-align: center!important;
  }*/
  .tab .title{
    margin-bottom: 65px;
  }
  .logs {
    height: 527px!important;
    margin-top: 20px;
  }
  .logs .el-tabs__content{
    width: 68%;
    height: calc(100vh*0.78);
    overflow: auto!important;
    background: white;
  }
  .header{
    height: 62px;
    background:#526ed7;
    font-size: 20px;
    line-height: 62px;
    text-align: center;
  }
  .download {
    width: 300px;
    background: #fff;
    height: calc(100vh*0.84);
    margin-top: 20px;
    margin-left: 5px;
  }
  .download ul li{
  list-style: none;
  }
  .download ul li a{
    display: inline-block;
    float: right;
  }
  .tab .el-tabs--left .el-tabs__nav.is-left{
    height: calc(100vh*0.78);
    background: white;
  }
</style>
