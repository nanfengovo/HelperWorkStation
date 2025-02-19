<template>
    <div class="grid-container">
    <div class="item">2</div>
    <div class="item">3</div>
    <div class="item">6</div>
    <div class="item">7</div>
    <div class="item">22</div>
    <div class="item">23</div>
    <div class="item">33</div>
    <div class="item">34</div>
    <div class="item">4</div>
    <div class="item">5</div>
    <div class="item highlighted">1</div>
</div>
</template>
<script setup lang = "ts">
</script>
<style>
.grid-container {
  display: grid;
  grid-template-columns: repeat(5, 50px); /* 调整列宽 */
  grid-gap: 10px; /* 项目之间的间距 */
  width: max-content;
}

.item {
  background-color: #f0f0f0;
  border: 1px solid #ccc;
  text-align: center;
  line-height: 50px; /* 与列宽相同以垂直居中文字 */
  font-size: 18px;
}

.highlighted {
  background-color: gray; /* 高亮显示特定项 */
}

</style>