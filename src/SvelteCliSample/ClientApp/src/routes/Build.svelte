<script>
    import { onMount } from 'svelte';
    import SortableList from 'svelte-sortable-list';
    import Test from '../components/Test.svelte';
    import { availableTests } from '../store/tests.js'
    import Select from 'svelte-select';

    
    let params;
    let exportName = "TestList";
    let partNumberInput = "";
    let selectedTests = [];
    $: isCreateDisabled = ( selectedTests.length < 1 )
    $: console.log("is create disabled", isCreateDisabled);

    const sortSelectedList = ev => { selectedTests = (ev.detail) };
    const handleExport = () => { downloadObjectAsJson(selectedTests, exportName) };
    const groupBy = (item) => item.group;

    const stationItems = [
    {value: 'PREP', label: 'Prepare for Production', group: 'Testing'},
    {value: 'EOLT', label: 'End of Line Tester', group: 'Testing'},
    {value: 'PROG', label: 'End of Line Programmer', group: 'Programming'},
    {value: 'LEAK', label: 'Leak Tester', group: 'Assembly'},
    {value: 'VIZN', label: 'Visual Inspection', group: 'Assembly'},
    {value: 'GLUE', label: 'Automated Glue Machine', group: 'Assembly'},
    {value: 'BONE', label: 'PCBA Programmer', group: 'Programming'}
    ];
    
    const cellItems = [
    {value: '5812', label: 'Skinny Peat / Black Label', group: 'M2'},
    {value: '5813', label: 'MPC10 / MPC20', group: 'Mid Cost'},
    {value: '5814', label: 'PV480 / PV485 / HV700', group: 'Low Cost'},
    {value: '5823', label: 'Furious', group: 'M2'},
    {value: '5824', label: 'Scallywag', group: 'M2'},
    {value: '5875', label: 'GPS Puck', group: 'Renesas'},
    {value: '5830', label: 'LM7', group: 'E2'},
    {value: '5831', label: 'LM4', group: 'E2'}
	];

    function addTest(item) {
        const retVal = selectedTests.find( obj => obj === item)

        if (retVal){
            alert(`${item.testName} is already selected`)
            return
        }

        selectedTests.push(item)
        selectedTests = selectedTests;
    }

    function removeTest(item) {
        const objIndex = selectedTests.findIndex( obj => obj === item)

        if (objIndex > -1) {
            selectedTests.splice(objIndex, 1);
            selectedTests = selectedTests;
        }
    }

    function downloadObjectAsJson(exportObj, exportName){
        var dataStr = "data:text/json;charset=utf-8," + encodeURIComponent(JSON.stringify(exportObj, null, "\t"));
        var downloadAnchorNode = document.createElement('a');
        downloadAnchorNode.setAttribute("href",     dataStr);
        downloadAnchorNode.setAttribute("download", exportName + ".json");
        document.body.appendChild(downloadAnchorNode); // required for firefox
        downloadAnchorNode.click();
        downloadAnchorNode.remove();
    }

</script>

<div>
    <h1>Test Builder</h1>
    <form>
        <div>
        <label>Part Number</label>
        <input type="text" bind:value={partNumberInput} placeholder="e.g. 78350777"/>
        </div>
        <div class="select">
        <label>Station Type</label>
        <Select items={stationItems} {groupBy}></Select>
        </div>
        <div class="select">
        <label>Production Cell</label>
        <Select items={cellItems} {groupBy}></Select>
        </div>
        <div>
        <label> File Name</label>
        <input type="text" bind:value={exportName} placeholder="file name"/>
        </div>
        <button type="button" class="btn btn-primary" on:click={handleExport} disabled={isCreateDisabled}>Save Test List</button>
        <small id="emailHelp" class="form-text text-muted">After you select tests, you can download the list for production.</small>
    </form>

<div class="lists">
    <div class="tests" ref="tests">
        <h2>Available Tests</h2>
        <small class="form-text text-muted section">These are the default tests supported at the moment.<br>Feel free to edit some limits and add them to your current test list.</small>

        {#if $availableTests}
            {#each $availableTests as item, index}
            <Test {item} {index} collapsible={true}>
                <button slot="actionButton" type="button" class="btn btn-warning" on:click={()=> addTest(item)}>
                Add To Selected Tests
                </button>
            </Test>
            {/each}
        {:else}
            <p><em>Loading...</em></p>
        {/if}
    </div>
    <div class="tests">
        <h2 class="text-warning">Selected Tests</h2>
        <small class="form-text text-muted section">These are the tests that get saved.<br>Drag them into the order you want to test them!</small>

        {#if selectedTests.length}
            <SortableList 
            bind:list={selectedTests} 
                key="id" 
            on:sort={sortSelectedList}
            let:item
            let:index >
                <Test {item} {index} collapsible={false}>
                    <span slot="indexNumber" class="orderIndex">{index+1}</span>
                    <button slot="actionButton" type="button" class="btn btn-danger" on:click={()=> removeTest(item)}>
                    Remove From Selected Tests
                    </button>
                </Test>
            </SortableList>
        {:else}
            <div style="text-align:center; height: 300px; display: flex; align-items: center;">
            <em style="padding:4rem;">No tests selected</em>
            </div>
        {/if}
    </div>
</div>


</div>

<style>
form{
    display:inline-block;
    margin-bottom: 4rem;
}
form div{
    display: flex;
    flex-direction: column;
    padding: 1rem 0;
}

.lists{
    width: 100%;
    display: flex;
    flex-direction: row;
    justify-content: space-around;
}
.tests{
    width: 33rem;
    padding-left: 2rem;
    padding-right: 2rem;
}
.select{
    --borderRadius: default;
    --placeholderColor: default;
    padding: 1rem 0;
}

.section{
    /* fixed subtext height for alignment, check you don't have text collision if you adjust this */
    height: 2.2rem; 
    margin-bottom: .8rem;
    text-overflow: '';
    overflow: hidden;
}

small{
    padding-bottom: 2rem;
}

.btn-danger:hover{
    border: solid 1px rgba(220, 53, 69, 1);
    box-shadow: 0 5px 15px rgba(220, 53, 69, 1);
}

.orderIndex{
    display: inline-block;
    align-content: center;
    background-color: #ddd;
    border-radius: 4px;
    width: 4rem;
    text-align: center;
}

button:disabled,
button[disabled]{
  opacity: 0.65; 
  cursor: not-allowed;
}
</style>