import React, {Component} from 'react';
import { withRouter } from "react-router";
import {Field} from 'react-final-form'
import Wizard from '../../components/wizard'

const Error = ({name}) => (
    <Field
        name={name}
        subscribe={{touched: true, error: true}}
        render={({meta: {touched, error}}) =>
            touched && error ? <span>{error}</span> : null
        }
    />
)

class Test extends Component {
    static displayName = Test.name;
    
    constructor(props) {
        super(props);        
        this.state = {template: [], loading: true};
    }

    componentDidMount() {
        const { id } = this.props.match.params;
        this.getTestTemplate(id);
    }

    renderTestTemplate(template) {
        return (
            <div>
                <h1>{template.title}</h1>
                <p>{template.description}</p>
                <Wizard
                    initialValues={{
                        testId: 1,
                        questions:template.questions.map(question=>{
                            return {questionId:question.id}
                        })
                    }}
                    onSubmit={this.onSubmit}
                >
                    {template.questions.map((question,index) =>
                        <Wizard.Page key={`question-${question.id}`}>
                            <h4>{question.title}</h4>
                            {question.answers.map(answer =>
                                <p key={`answer-${answer.id}`}>
                                    <Field name={`questions[${index}].answerId`} component="input" type="radio"
                                           value={`${answer.id}`} validate={this.validateQuestion}/> {answer.content}
                                </p>
                            )}
                            <Error name={`questions[${index}].answerId`}/>
                    
                        </Wizard.Page>
                    )}
                </Wizard>
            </div>
        );
    }

    validateQuestion(value) {
        return (value ? undefined : 'Please select an answer');
    } 
    
    onSubmit(values){
        window.alert(JSON.stringify(values, 0, 2))
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderTestTemplate(this.state.template);

        return (
            <div>
                {contents}
            </div>
        );
    }

    async getTestTemplate(id) {        
        const template = await fetch(`https://localhost:7137/templates/${id}`);
        const templateData = await template.json();

        this.setState({template: templateData, loading: false});
    }
}

export default withRouter(Test);