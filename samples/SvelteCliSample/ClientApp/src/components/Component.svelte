<script>
	export let item;
    export let index;
    
    console.log("item found:", item)
    console.log("item found:", index)
</script>

<style>
	span{
		background-color: #ddd;
		border-radius: 4px;
		padding: 0 1em;
		font-size: .5em;
	}
</style>

<h1>
	<span>{index+1}</span> {item.name}
</h1>
<p>UpperLimit: {item.upperLimit}</p>
<p>LowerLimit: {item.lowerLimit}</p>