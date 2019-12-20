<script>
    import {slide} from 'svelte/transition';
    import { availableTests } from '../store/tests.js'

	export let item;
    export let index;
    export let selected = false;

    let addDescrVisible = false;
    
      const toggleSelected = () => {

          //selected -> unselected


          //unselected -> selected

          
        item.testSelected = !item.testSelected;
        
        //create copy of tests, make it an array type
        let tests = $availableTests;
        console.log("tests",tests)

       //find index location by id
    //    console.log(`searching for id ${item.id}`)
    //     let itemIndex = tests.findIndex( i => {
    //         console.log("actual i.id", i.id)
    //         i.id === item.id}
    //         )
    //     console.log("item index", itemIndex)

        //find item by id
        let itemAtIndex = tests.find( i => i.id === item.id)
        console.log("item at index", itemAtIndex)

        itemAtIndex.testSelected = !item.testSelected

        //insert copy with replaceAt
        const newArray = replaceAt(tests, index, itemAtIndex);
        console.log("new array", newArray)

        //set store with copy
        availableTests.set(newArray);

    };

    // a little function to help us with reordering the result
    const reorder = (list, startIndex, endIndex) => {
        const result = Array.from(list);
        const [removed] = result.splice(startIndex, 1);
        result.splice(endIndex, 0, removed);

        return result;
    };

    function replaceAt(array, index, value) {
        const ret = array.slice(0);
        console.log("ret",ret)
        ret[index] = value;
        return ret;
    }

    const disableDrag = ev => {
        ev.preventDefault();
        ev.stopPropagation();
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
    .selected:hover{
        cursor: move;
        /* same color as "btn-warning", but rgba for opacity */
        border: solid 1px rgb(255, 193, 7);
        background: rgba(255, 193, 7, 0.05);
    }
    .orderIndex{
        align-content: center;
    }
    .btn-danger:hover{
        border: solid 1px rgba(220, 53, 69, 1);
        box-shadow: 0 5px 15px rgba(220, 53, 69, 1);
    }

</style>

<div class="test-entry" class:selected="{selected === true}">
    <h1>
        {#if item.testSelected}<span class="orderIndex">{index+1}</span>{/if}
        {item.testName}
    </h1>
    {#if !item.testSelected}
    <button type="button" class="btn btn-outline-secondary" on:click={() => addDescrVisible = !addDescrVisible}>
	Edit
    </button>
    <button type="button" class="btn btn-warning" on:click={toggleSelected}>
	Add To Selected Tests
    </button>
    {:else}
    <button type="button" class="btn btn-outline-secondary" on:click={() => addDescrVisible = !addDescrVisible}>
	View Details
    </button>
    <button type="button" class="btn btn-danger" on:click={toggleSelected}>
	Remove From Selected Tests
    </button>
    {/if}

    {#if addDescrVisible}
    <div transition:slide>
        <p>Test Value Type: {item.testValueType}</p>
        <p>Upper Limit: </p> <input type="text" bind:value={item.upperLimit} placeholder={item.testValueType}>
        <p>Lower Limit: {item.lowerLimit}</p>
    </div>
    {/if}

</div>