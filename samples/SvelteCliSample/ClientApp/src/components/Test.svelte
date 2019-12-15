<script>
    import { selectedTests } from '../store/stores.js';
    import AddTest from './AddTest.svelte';

	export let item;
    export let index;
    
    console.log("list item", item)
</script>

<style>
	span{
		background-color: #ddd;
		border-radius: 4px;
		padding: 0 1em;
		font-size: .5em;
	}
    .test-entry{
        padding: 1rem;
        border: solid 1px black;
        border-radius: 1rem;
    }
</style>

<div class="test-entry">
    <h1>
        <span>{index+1}</span> {item.testName}
    </h1>
    <p>UpperLimit: {item.upperLimit}</p>
    <p>LowerLimit: {item.lowerLimit}</p>
    <AddTest />
</div>