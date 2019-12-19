<script>
    // import { selectedTests } from '../store/stores.js';
    import AddTest from './AddTest.svelte';
    import RemoveTest from './RemoveTest.svelte';
    import {slide} from 'svelte/transition';
    import { availableTests } from '../store/tests.js'

	export let item;
    export let index;

    let addDescrVisible = false;
    
      const toggleSelected = () => {
        // item.testSelected = !item.testSelected;
        
        //create copy of tests, make it an array type
        let tests = $availableTests;
        console.log("tests",tests)

       //find index location by id
       console.log(`searching for id ${item.id}`)
        let itemIndex = tests.findIndex( i => i.id === item.id)
        console.log("item index", itemIndex)

        //find item by id
        let itemAtIndex = tests.find( i => i.id === itemIndex)
        console.log("item at index", itemAtIndex)

        itemAtIndex.testSelected = !item.testSelected

        //insert copy with replaceAt
        const newArray = replaceAt(tests, index, itemAtIndex);
        console.log("new array", newArray)

        //set store with copy
        availableTests.set(newArray);

    };

    function replaceAt(array, index, value) {
        const ret = array.slice(0);
        console.log("ret",ret)
        ret[index] = value;
        return ret;
    }


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
        border: solid 1px #AAA;
        border-radius: 1rem;
    }

</style>

<div class="test-entry">
    <h1>
        <span>{index+1}</span> {item.testName}
    </h1>
    <button type="button" class="btn btn-outline-secondary" on:click={() => addDescrVisible = !addDescrVisible}>
	Expand / Collapse
    </button>
    {#if !item.testSelected}
    <button type="button" class="btn btn-warning" on:click={toggleSelected}>
	Add To Selected Tests
    </button>
    {:else}
    <button type="button" class="btn btn-danger" on:click={toggleSelected}>
	Remove From Selected Tests
    </button>
    {/if}

    {#if addDescrVisible}
    <div transition:slide>
        <fieldset class="form-group">
            <legend>Sliders</legend>
            <label for="upperLimit">Upper Limit</label>
            <input type="range" class="custom-range" id="upperLimit" data-com.bitwarden.browser.user-edited="yes">
        </fieldset>
        <p>UpperLimit: {item.upperLimit}</p>
        <p>LowerLimit: {item.lowerLimit}</p>
    </div>
    {/if}

</div>