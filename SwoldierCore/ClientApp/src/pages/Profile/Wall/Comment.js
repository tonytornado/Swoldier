import React, { useState } from 'react';
import MarkdownContainer from '../../../components/modules/MarkdownContainer';
import { InputStack } from "./InputStack";

/**
 * Renders a comment with replies
 *
 * @param {Array} props A comment, of course.
 */

export function Comment(props) {
    const [comment, setComment] = useState(props.comment);
    const [reply, setReply] = useState(comment.replies);

    if (comment) {
        return (
            <article key={comment.commentId}>
                <MarkdownContainer props={comment.text} />
                <div>
                    <small>{comment.postDate} {comment.Edited ?? "Edited"}</small>
                </div>
                <div>
                    <button className="btn btn-sm" value="Comment" />
                </div>
            </article>
        );
    }

    return (
        <InputStack type={"Comment"} />
    );
}
