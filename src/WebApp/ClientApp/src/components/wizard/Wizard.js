import React from 'react'
import {Form} from 'react-final-form'

export default class Wizard extends React.Component {

    constructor(props) {
        super(props)
        this.state = {
            page: 0,
            values: props.initialValues || {}
        }
    }

    static Page = ({children}) => children

    next = values =>
        this.setState(state => ({
            page: Math.min(state.page + 1, this.props.children.length - 1),
            values
        }))

    previous = () =>
        this.setState(state => ({
            page: Math.max(state.page - 1, 0)
        }))

    /**
     * NOTE: Both validate and handleSubmit switching are implemented
     * here because 🏁 Redux Final Form does not accept changes to those
     * functions once the form has been defined.
     */

    validate = values => {
        const activePage = React.Children.toArray(this.props.children)[
            this.state.page
            ]
        return activePage.props.validate ? activePage.props.validate(values) : {}
    }

    handleSubmit = values => {
        const {children, onSubmit} = this.props
        const {page} = this.state
        const isLastPage = page === React.Children.count(children) - 1
        if (isLastPage) {
            return onSubmit(values)
        } else {
            this.next(values)
        }
    }

    render() {
        const {children} = this.props
        const {page, values} = this.state
        const activePage = React.Children.toArray(children)[page]
        const isLastPage = page === React.Children.count(children) - 1
        return (
            <Form
                initialValues={values}
                validate={this.validate}
                onSubmit={this.handleSubmit}
            >
                {({handleSubmit, submitting}) => (
                    <form onSubmit={handleSubmit}>
                        <div>
                        {activePage}
                        </div>
                        <div className="buttons">
                            {page > 0 && (
                                <button type="button" onClick={this.previous} className="btn btn-primary mr-2 ml-2">
                                    « Previous
                                </button>
                            )}
                            {!isLastPage && <button type="submit" className="btn btn-primary mr-2">Next »</button>}
                            {isLastPage && (
                                <button type="submit" disabled={submitting} className="btn btn-danger">
                                    Submit
                                </button>
                            )}
                        </div>

                        {/*<pre>{JSON.stringify(values, 0, 2)}</pre>*/}
                    </form>
                )}
            </Form>
        )
    }
}
